using Pastelaria.Domain.Usuario;
using Pastelaria.Domain.Usuario.Dto;
using Pastelaria.Repository.Infra;
using Pastelaria.Repository.Infra.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Pastelaria.Repository
{
    // Classe de repositório para gerenciar dados de usuários
    public class UsuarioRepository : BaseRepository, IUsuarioRepository
    {
        // Construtor que injeta a conexão com o banco de dados
        public UsuarioRepository(IDatabaseConnection connection) : base(connection)
        {
        }

        // Enumeração que define os nomes dos procedimentos armazenados
        private enum Procedures
        {
            BuscarUsuario,          // Buscar usuário
            AdicionarUsuario,         // Inserir novo usuário
            AtualizarUsuario,       // Atualizar usuário
            DesativarUsuario,       // Desativar usuário
        }

        // Obter todos os usuários
        public IEnumerable<UsuariosDto> Get()
        {
            ExecuteProcedure(Procedures.BuscarUsuario);  // Executar procedimento armazenado para buscar usuários
            using (var r = ExecuteReader())              // Executar leitor para ler os resultados
                return r.CastEnumerable<UsuariosDto>();  // Converter resultados para IEnumerable de UsuariosDto
        }

        // Inserir um novo usuário
        public void Post(UsuariosDto usuario)
        {
            ExecuteProcedure(Procedures.AdicionarUsuario); // Executar procedimento armazenado para inserir usuário
            // Adicionar parâmetros para os dados do usuário
            AddParameter("@Nome", usuario.Nome);
            AddParameter("@Email", usuario.Email);
            AddParameter("@Senha", usuario.Senha);
            AddParameter("@DataNascimento", usuario.DataNascimento);
            AddParameter("@Ind_Ativo", usuario.Ind_Ativo);
            AddParameter("@IdPerfil", usuario.IdPerfil);
            AddParameter("@IdGestor", usuario.IdGestor);
            AddParameter("@Telefone", usuario.Telefone);
            AddParameter("@TipoTelefone", usuario.TipoTelefone);
            AddParameter("@Cep", usuario.Cep);
            AddParameter("@Cidade", usuario.Cidade);
            AddParameter("@Bairro", usuario.Bairro);
            AddParameter("@Rua", usuario.Rua);
            AddParameter("@Numero", usuario.Numero);
            AddParameter("@Complemento", usuario.Complemento);

            ExecuteNonQuery();  // Executar comando não-query (operação de inserção)
        }

        // Atualizar um usuário existente
        public void Put(UsuariosDto usuario)
        {
            ExecuteProcedure(Procedures.AtualizarUsuario);  // Executar procedimento armazenado para atualizar usuário
            // Adicionar parâmetros para os dados do usuário
            AddParameter("@IdUsuario", usuario.IdUsuario);
            AddParameter("@Nome", usuario.Nome);
            AddParameter("@Email", usuario.Email);
            AddParameter("@Senha", usuario.Senha);
            AddParameter("@DataNascimento", usuario.DataNascimento);
            AddParameter("@IdPerfil", usuario.IdPerfil);
            AddParameter("@IdGestor", usuario.IdGestor);
            AddParameter("@Telefone", usuario.Telefone);
            AddParameter("@TipoTelefone", usuario.TipoTelefone);
            AddParameter("@Cep", usuario.Cep);
            AddParameter("@Cidade", usuario.Cidade);
            AddParameter("@Bairro", usuario.Bairro);
            AddParameter("@Rua", usuario.Rua);
            AddParameter("@Numero", usuario.Numero);
            AddParameter("@Complemento", usuario.Complemento);
            ExecuteNonQuery();  // Executar comando não-query (operação de atualização)
        }

        // Desativar um usuário
        public void PutDesativaUsuario(int idUsuario)
        {
            ExecuteProcedure(Procedures.DesativarUsuario);  // Executar procedimento armazenado para desativar usuário
            AddParameter("@IdUsuario", idUsuario);          // Adicionar parâmetro para o ID do usuário
            ExecuteNonQuery();  // Executar comando não-query
        }

        // Obter usuário por ID ou nome
        public UsuariosDto Get(int? id = null, string nome = null)
        {
            ExecuteProcedure(Procedures.BuscarUsuario);  // Executar procedimento armazenado para buscar usuário
            // Adicionar parâmetros para filtrar por ID ou nome
            AddParameter("@IdUsuario", id);
            AddParameter("@Nome", nome);
            using (var r = ExecuteReader())              // Executar leitor para ler o resultado
                return r.Read() ? r.Cast<UsuariosDto>() : null;  // Converter resultado para UsuariosDto ou retornar null se não houver resultado
        }

        // Obter usuários pelo nome
        public IEnumerable<UsuariosDto> GetNome(string nome)
        {
            ExecuteProcedure(Procedures.BuscarUsuario);  // Executar procedimento armazenado para buscar usuários
            AddParameter("@Nome", nome);                 // Adicionar parâmetro para filtrar pelo nome
            using (var r = ExecuteReader())              // Executar leitor para ler os resultados
                return r.CastEnumerable<UsuariosDto>();  // Converter resultados para IEnumerable de UsuariosDto
        }
    }
}
