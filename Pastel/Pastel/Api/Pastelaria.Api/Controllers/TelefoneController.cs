using Pastelaria.Domain.Telefone;
using Pastelaria.Domain.Telefone.Dto; 
using System.Web.Http; // Importa o namespace necessário para usar funcionalidades do ASP.NET Web API.

namespace Pastelaria.Api.Controllers
{
    [RoutePrefix("api/telefone")] // Define o prefixo de rota para todas as rotas neste controlador como "api/telefone".
    public class TelefoneController : ApiController
    {
        private readonly ITelefoneRepository _telefoneRepository; // Declara uma variável privada para armazenar o repositório de telefone.

        public TelefoneController(ITelefoneRepository telefoneRepository)
        {
            _telefoneRepository = telefoneRepository; // Inicializa o repositório de telefone através da injeção de dependência no construtor.
        }

        [HttpPost, Route("adicionar")] // Define que este método responde a requisições HTTP POST na rota "api/telefone/adicionar".
        public IHttpActionResult Post(TelefoneDto telefone)
        {
            _telefoneRepository.Post(telefone); // Chama o método Post do repositório para adicionar um novo telefone.
            return Ok(); // Retorna uma resposta HTTP 200 OK para indicar que a operação foi realizada com sucesso.
        }
    }
}
