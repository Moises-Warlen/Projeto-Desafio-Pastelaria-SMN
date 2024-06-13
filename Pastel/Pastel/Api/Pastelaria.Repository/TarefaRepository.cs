using Pastelaria.Repository.Infra;
using System.Collections.Generic;
using System.Linq;
using Pastelaria.Domain.Usuario.Dto;
using Pastelaria.Domain.Tarefa;
using Pastelaria.Repository.Infra.Extensions;
using System;

namespace Pastelaria.Repository
{
    public class TarefaRepository : BaseRepository, ITarefaRepository
    {
        public TarefaRepository(IDatabaseConnection connection) : base(connection)
        {
            // Construtor da classe, recebe uma conexão com o banco de dados
        }

        private enum Procedures
        {
            AdicionaTarefa,                      // Enumeração dos procedimentos armazenados disponíveis
            ConcluirTarefa,
            BuscarTodasTarefasAtivas,
            BuscarTarefasAtivasDoUsuario
        }

        public IEnumerable<TarefaDto> Get()
        {
            ExecuteProcedure(Procedures.BuscarTodasTarefasAtivas);  // Executa procedimento para buscar todas as tarefas ativas
            using (var r = ExecuteReader())             // Utiliza um leitor para obter os resultados
                return r.CastEnumerable<TarefaDto>();   // Converte os resultados para IEnumerable de TarefaDto
        }

        public TarefaDto Get(int id)
        {
            ExecuteProcedure(Procedures.BuscarTarefasAtivasDoUsuario);  // Executa procedimento para buscar tarefas ativas de um usuário específico
            AddParameter("@IdUsuario", id);        // Adiciona parâmetro para filtrar pelo ID do usuário
            using (var r = ExecuteReader())             // Utiliza um leitor para obter o resultado
                return r.Read() ? r.Cast<TarefaDto>() : null;  // Converte o resultado para TarefaDto ou retorna null se não houver resultado
        }

        // Método para inserir uma nova tarefa
        public void Post(TarefaDto tarefa)
        {
            ExecuteProcedure(Procedures.AdicionaTarefa); // Executa procedimento para adicionar uma nova tarefa
                                                         // Adiciona parâmetros com os dados da tarefa
            AddParameter("@IdUsuario", tarefa.IdUsuario);
            AddParameter("@Descricao", tarefa.Descricao);
            AddParameter("@DataAtribuicao", tarefa.DataAtribuicao);
            AddParameter("@DataConclusao", tarefa.DataConclusao);  // Pode ser nulo
            AddParameter("@CriadorId", tarefa.CriadorId);
            AddParameter("@Ind_Ativo", tarefa.Ind_Ativo);

            ExecuteNonQuery();  // Executa comando não-query (operação de inserção)
        }

        // Método para marcar uma tarefa como concluída
        public void PutConcluirTarefa(int IdTarefa)
        {
            ExecuteProcedure(Procedures.ConcluirTarefa);  // Executa procedimento para concluir uma tarefa
            AddParameter("@IdTarefa", IdTarefa);  // Adiciona parâmetro com o ID da tarefa a ser concluída
            AddParameter("@DataConclusao", DateTime.Now);  // Define a data de conclusão como a data e hora atuais
            ExecuteNonQuery();  // Executa comando não-query
        }

    }
}
