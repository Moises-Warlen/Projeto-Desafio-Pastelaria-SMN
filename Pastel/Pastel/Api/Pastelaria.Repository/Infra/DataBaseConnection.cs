using System;
using System.Data;
using System.Data.SqlClient;

namespace Pastelaria.Repository.Infra
{
    public class DatabaseConnection : IDatabaseConnection, IDisposable
    {
        public DatabaseConnection()
        {
            // Configuração da conexão com o banco de dados SQL Server
            SqlConnection = new SqlConnection(
                 "Data Source=DESKTOP-57GRG02;" + // Nome do servidor
                 "Initial Catalog=Pastelaria;" +
                 "Integrated Security=True;" + // Autenticação do Windows
                 "Connection Timeout=300"
            );
        }

        public SqlConnection SqlConnection { get; } // Objeto de conexão com o banco de dados
        public SqlTransaction SqlTransaction { get; set; } // Objeto para transação SQL

        // Método privado para abrir a conexão com o banco de dados
        private void OpenConnection()
        {
            // Verifica se a conexão está quebrada (broken), fecha e então abre novamente
            if (SqlConnection.State == ConnectionState.Broken)
            {
                SqlConnection.Close();
                SqlConnection.Open();
            }

            // Se a conexão estiver fechada, abre a conexão
            if (SqlConnection.State == ConnectionState.Closed)
                SqlConnection.Open();
        }

        // Método para iniciar uma transação SQL
        public void OpenTransaction()
        {
            OpenConnection(); // Abre a conexão com o banco de dados
            SqlTransaction = SqlConnection.BeginTransaction(); // Inicia uma nova transação
        }

        // Método para iniciar uma transação SQL com um nível de isolamento específico
        public void OpenTransaction(IsolationLevel isolationLevel)
        {
            OpenConnection(); // Abre a conexão com o banco de dados
            SqlTransaction = SqlConnection.BeginTransaction(isolationLevel); // Inicia uma nova transação com o isolamento especificado
        }

        // Método para confirmar (commit) uma transação
        public void Commit()
        {
            SqlTransaction.Commit(); // Confirma a transação
            SqlTransaction.Dispose(); // Libera os recursos da transação
        }

        // Método para reverter (rollback) uma transação
        public void Rollback()
        {
            SqlTransaction.Rollback(); // Reverte a transação
            SqlTransaction.Dispose(); // Libera os recursos da transação
        }

        // Implementação do método Dispose da interface IDisposable para fechar a conexão com o banco de dados
        public void Dispose()
        {
            SqlConnection.Close(); // Fecha a conexão com o banco de dados
        }
    }
}
