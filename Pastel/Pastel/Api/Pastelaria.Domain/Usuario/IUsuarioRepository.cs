using Pastelaria.Domain.Usuario.Dto;
using System.Collections.Generic;

namespace Pastelaria.Domain.Usuario
{
   public  interface IUsuarioRepository
    {
        // Método para obter todos os usuários
        // Retorna uma lista de objetos UsuariosDto
        IEnumerable<UsuariosDto> Get();

        // Método para obter usuários pelo nome
        // Aceita um parâmetro 'nome' e retorna uma lista de objetos UsuariosDto que correspondem ao nome
        UsuariosDto Get(int? id = null, string nome = null);

        // Método para adicionar um novo usuário
        // Aceita um objeto UsuariosDto representando o usuário a ser adicionado
        void Post(UsuariosDto usuario);

        // Método para desativar um usuário
        // Aceita um parâmetro 'IdUsuario' que representa o ID do usuário a ser desativado
        void PutDesativaUsuario(int IdUsuario);

        // Método para atualizar um usuário existente
        // Aceita um objeto UsuariosDto representando o usuário a ser atualizado
        void Put(UsuariosDto usuario);

    }
}
