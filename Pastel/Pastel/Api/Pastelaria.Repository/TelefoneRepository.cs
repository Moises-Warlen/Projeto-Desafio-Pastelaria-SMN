using Pastelaria.Domain.Telefone;  
using Pastelaria.Repository.Infra;
using Pastelaria.Domain.Telefone.Dto;

namespace Pastelaria.Repository
{
    public class TelefoneRepository : BaseRepository, ITelefoneRepository  // Definindo a classe e implementando a interface
    {
        public TelefoneRepository(IDatabaseConnection connection) : base(connection)  // Construtor para inicializar com conexão de banco de dados
        {
        }

        // Enumeração que define os procedimentos armazenados (supondo nomes de procedimentos)
        private enum Procedures
        {
            AdicionarTelefoneUsuarioExistente
        }

        public void Post(TelefoneDto telefone)
        {
            ExecuteProcedure(Procedures.AdicionarTelefoneUsuarioExistente);  // Chamando procedimento armazenado para adicionar telefone para usuário existente
            AddParameter("@IdUsuario", telefone.IdUsuario);  // Adicionando parâmetros para o procedimento armazenado
            AddParameter("@Telefone", telefone.Telefone);
            AddParameter("@TipoTelefone", telefone.TipoTelefone);

            ExecuteNonQuery();  // Executando comando não-query (operação de inserção)
        }
    }
}
