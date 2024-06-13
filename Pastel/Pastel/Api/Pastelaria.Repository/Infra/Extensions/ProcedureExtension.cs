using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;

namespace Pastelaria.Repository.Infra.Extensions
{
    public static class ProcedureExtension
    {
        // Método de extensão para ler um atributo do tipo T do IDataReader baseado no nome do atributo
        public static T ReadAttr<T>(this IDataReader r, string attrName)
        {
            try
            {
                // Verifica se o valor do atributo é DBNull ou vazio e retorna o valor padrão de T se for o caso
                if (r[attrName] == DBNull.Value || string.IsNullOrEmpty(r[attrName].ToString()))
                    return default(T);

                var tipoT = typeof(T);
                var tipoR = r[attrName].GetType();

                // Converte e retorna o valor do atributo para o tipo T, se possível
                return (T)(tipoR == tipoT || (tipoT.GetGenericArguments().Any() && tipoR == tipoT.GenericTypeArguments[0])
                    ? r[attrName]
                    : Convert.ChangeType(r[attrName], tipoT));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Método de extensão para fazer cast dos dados do IDataReader para um objeto do tipo T
        public static T Cast<T>(this IDataReader r) where T : class
        {
            var propName = "";
            try
            {
                var obj = (T)Activator.CreateInstance(typeof(T));
                var props = obj.GetType().GetProperties();

                // Itera sobre as colunas do IDataReader
                for (var i = 0; i < r.FieldCount; i++)
                {
                    var columnName = r.GetName(i);
                    // Verifica se o valor da coluna é DBNull ou nulo, e continua para a próxima coluna se for o caso
                    if (r[columnName] == DBNull.Value || r[columnName] == null)
                        continue;

                    // Encontra a propriedade correspondente ao nome da coluna
                    var prop = props.FirstOrDefault(x => string.Equals(x.Name, columnName, StringComparison.OrdinalIgnoreCase));
                    if (prop == null)
                        continue;

                    propName = prop.Name;
                    var propType = prop.PropertyType;
                    var columnType = r[columnName].GetType();

                    // Define o valor da propriedade no objeto do tipo T
                    prop.SetValue(obj, propType == columnType || (propType.GetGenericArguments().Any() && propType.GenericTypeArguments[0] == columnType)
                        ? r[columnName]
                        : Convert.ChangeType(r[columnName], propType));
                }

                return obj;
            }
            catch (Exception ex)
            {
                // Lança uma exceção com o nome da propriedade e a mensagem de erro detalhada
                throw new Exception($"{propName}: {ex.Message}");
            }
        }

        // Método de extensão para fazer cast dos dados do IDataReader para um objeto do tipo T ou retorna um novo objeto T vazio
        public static T CastEmpty<T>(this IDataReader r) where T : class, new() => r.Read() ? r.Cast<T>() : new T();

        // Método de extensão para fazer cast dos dados do IDataReader para uma coleção de objetos do tipo T
        public static IEnumerable<T> CastEnumerable<T>(this IDataReader r) where T : class
        {
            var collection = new Collection<T>();
            while (r.Read())
                collection.Add(r.Cast<T>());
            return collection;
        }

        // Método de extensão para fazer cast de uma coluna específica do IDataReader para uma coleção de objetos do tipo T
        public static IEnumerable<T> CastEnumerable<T>(this IDataReader r, string column)
        {
            var collection = new Collection<T>();
            while (r.Read())
                collection.Add(r.ReadAttr<T>(column));
            return collection;
        }
    }
}
