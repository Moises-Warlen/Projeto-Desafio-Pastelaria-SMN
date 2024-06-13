using Pastelaria.Domain.Endereco; 
using Pastelaria.Domain.Endereco.Dto;
using Pastelaria.Repository.Infra; 
namespace Pastelaria.Repository
{
    // Classe que implementa a interface IEnderecoRepository e é responsável por manipular dados de endereço.
    public class EnderecoRepository : BaseRepository, IEnderecoRepository
    {
        // Construtor que recebe uma conexão com o banco de dados através da classe base BaseRepository.
        public EnderecoRepository(IDatabaseConnection connection) : base(connection)
        {
        }

        // Enumeração que lista os procedimentos disponíveis para execução.
        private enum Procedures
        {
            AdicionarEnderecoUsuarioExistente
        }
        public void Post(EnderecoDto endereco)
        {
            ExecuteProcedure(Procedures.AdicionarEnderecoUsuarioExistente); // Executar procedimento armazenado para inserir endereço
            // Adicionar parâmetros para os dados do endereço
            AddParameter("@IdUsuario", endereco.IdUsuario);
            AddParameter("@Cep", endereco.Cep);
            AddParameter("@Cidade", endereco.Cidade);
            AddParameter("@Bairro", endereco.Bairro);
            AddParameter("@Rua", endereco.Rua);
            AddParameter("@Numero", endereco.Numero);
            AddParameter("@Complemento", endereco.Complemento);

            ExecuteNonQuery();  // Executar comando não-query (operação de inserção)
        }
    }
}
