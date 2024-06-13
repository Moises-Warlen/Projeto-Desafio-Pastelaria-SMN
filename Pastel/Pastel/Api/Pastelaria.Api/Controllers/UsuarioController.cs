using Pastelaria.Domain.Usuario;
using Pastelaria.Domain.Usuario.Dto;
using System.Web.Http;

namespace Pastelaria.Api.Controllers
{
    // Controlador responsável por gerenciar operações relacionadas aos usuários da aplicação.
    [RoutePrefix("api/usuario")] // Define um prefixo de rota para todos os endpoints deste controlador.
    public class UsuarioController : ApiController
    {
        private readonly IUsuarioRepository _usuarioRepository;

        // Construtor que recebe uma instância do repositório de usuários para realizar operações de acesso a dados.
        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        // Endpoint para obter todos os usuários cadastrados na aplicação.
        [HttpGet, Route("todos")]
        public IHttpActionResult Get()
        {
            var usuarios = _usuarioRepository.Get(); // Chama o método Get do repositório para obter todos os usuários.
            return Ok(usuarios); // Retorna a lista de usuários com status 200 OK.
        }

        // Endpoint para obter um usuário específico ou uma lista de usuários com base em filtros.
        [HttpGet, Route("")]
        public IHttpActionResult Get(int? id = null, string nome = null, byte? codigoPerfil = null)
        {
            var usuarios = _usuarioRepository.Get(id, nome); // Chama o método Get com parâmetros opcionais para obter usuários filtrados.
            return Ok(usuarios); // Retorna a lista de usuários (ou um usuário específico) com status 200 OK.
        }

        // Endpoint para excluir um usuário com base em seu ID.
        [HttpDelete, Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            _usuarioRepository.PutDesativaUsuario(id); // Chama o método PutDesativaUsuario do repositório para desativar o usuário.
            return Ok(); // Retorna status 200 OK.
        }

        // Endpoint para adicionar um novo usuário à aplicação.
        [HttpPost, Route("adicionar")]
        public IHttpActionResult Post(UsuariosDto usuario)
        {
            _usuarioRepository.Post(usuario); // Chama o método Post do repositório para adicionar um novo usuário.
            return Ok(); // Retorna status 200 OK.
        }

        // Endpoint para atualizar informações de um usuário existente na aplicação.
        [HttpPut, Route("alterar")]
        public IHttpActionResult Put(UsuariosDto usuario)
        {
            _usuarioRepository.Put(usuario); // Chama o método Put do repositório para atualizar um usuário existente.
            return Ok(); // Retorna status 200 OK.
        }
    }
}
