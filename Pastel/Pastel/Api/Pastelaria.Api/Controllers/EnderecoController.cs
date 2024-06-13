using Pastelaria.Domain.Endereco;  
using Pastelaria.Domain.Endereco.Dto;  
using System.Web.Http;  // Importa o namespace para utilização da Web API do ASP.NET.

namespace Pastelaria.Api.Controllers
{
    [RoutePrefix("api/endereco")]  // Define o prefixo de rota para este controlador como "api/endereco".
    public class EnderecoController : ApiController
    {
        private readonly IEnderecoRepository _enderecoRepository;  // Declaração de uma variável privada para armazenar o repositório de endereços.

        public EnderecoController(IEnderecoRepository enderecoRepository)
        {
            _enderecoRepository = enderecoRepository;  // Injeta o repositório de endereços via construtor.
        }

        // Endpoint para adicionar um novo endereço à aplicação.
        [HttpPost, Route("adicionar")]  // Atributos de rota para indicar que este método responde a requisições HTTP POST para "api/endereco/adicionar".
        public IHttpActionResult Post(EnderecoDto endereco)
        {
            _enderecoRepository.Post(endereco); // Chama o método Post do repositório para adicionar um novo endereço.
            return Ok(); // Retorna status 200 OK para indicar que a operação foi bem-sucedida.
        }
    }
}
