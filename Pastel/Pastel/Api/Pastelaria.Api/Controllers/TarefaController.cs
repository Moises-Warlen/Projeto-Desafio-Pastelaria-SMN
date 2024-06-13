using Pastelaria.Domain.Tarefa;
using Pastelaria.Domain.Usuario.Dto;
using System.Web.Http;

namespace Pastelaria.Api.Controllers
{
    // Controlador responsável por gerenciar operações relacionadas às tarefas da aplicação.
    [RoutePrefix("api/tarefa")] // Define um prefixo de rota para todos os endpoints deste controlador.
    public class TarefaController : ApiController
    {
        private readonly ITarefaRepository _tarefaRepository;

        // Construtor que recebe uma instância do repositório de tarefas para realizar operações de acesso a dados.
        public TarefaController(ITarefaRepository tarefaRepository)
        {
            _tarefaRepository = tarefaRepository;
        }

        // Endpoint para obter todas as tarefas cadastradas na aplicação.
        [HttpGet, Route("todas")]
        public IHttpActionResult Get()
        {
            var tarefas = _tarefaRepository.Get(); // Chama o método Get do repositório para obter todas as tarefas.
            return Ok(tarefas); // Retorna a lista de tarefas com status 200 OK.
        }

        // Endpoint para obter uma tarefa específica com base em seu ID.
        [HttpGet, Route("{id}")]
        public IHttpActionResult Get(int id)
        {
            var tarefa = _tarefaRepository.Get(id); // Chama o método Get do repositório para obter uma tarefa específica.
            return Ok(tarefa); // Retorna a tarefa com status 200 OK.
        }

        // Endpoint para adicionar uma nova tarefa à aplicação.
        [HttpPost, Route("adicionar")]
        public IHttpActionResult Post(TarefaDto tarefa)
        {
            _tarefaRepository.Post(tarefa); // Chama o método Post do repositório para adicionar uma nova tarefa.
            return Ok(); // Retorna status 200 OK.
        }

        // Endpoint para concluir uma tarefa com base em seu ID.
        [HttpPut, Route("concluir/{id}")]
        public IHttpActionResult PutConcluir(int id)
        {
            _tarefaRepository.PutConcluirTarefa(id); // Chama o método PutConcluirTarefa do repositório para concluir a tarefa.
            return Ok(); // Retorna status 200 OK.
        }
    }
}
