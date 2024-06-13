using Pastelaria.Domain.Usuario.Dto;

using System.Collections.Generic;


namespace Pastelaria.Domain.Tarefa
{
    public interface ITarefaRepository
    {
        // Obtém todas as tarefas
        IEnumerable<TarefaDto> Get();

        // Obtém uma tarefa pelo seu ID
        TarefaDto Get(int id);

        // Adiciona uma nova tarefa
        void Post(TarefaDto tarefa);

        // Marca uma tarefa como concluída para um determinado ID de usuário
        void PutConcluirTarefa(int IdUsuario);
    }
}
