namespace WebApiWithGenerics.WebApi.Repositories
{
    using System.Threading.Tasks;

    using Dapper;

    using Microsoft.Extensions.Logging;

    using MySqlConnector;

    using WebApiWithGenerics.WebApi.Configuration;
    using WebApiWithGenerics.WebApi.Contracts.Todo;
    using WebApiWithGenerics.WebApi.CustomExceptions;
    using WebApiWithGenerics.WebApi.SqlScriptGenerators;

    public class TodoRepository : DapperMySqlCrudRepository<TodoDbContract>, ITodoRepository
    {
        private readonly string selectByTodoListIdScript;

        public TodoRepository(ILogger<TodoRepository> logger, DatabaseConfiguration options, TodoDapperMySqlScriptGenerator userDapperMySqlScriptGenerator)
            : base(logger, options, userDapperMySqlScriptGenerator)
        {
            this.selectByTodoListIdScript = userDapperMySqlScriptGenerator.GenerateSelectByTodoListIdScript();
        }

        public async Task<TodoDbContract> GetByTodoListIdAsync(string todoListId)
        {
            await using var connection = new MySqlConnection(this.ConnectionString);
            var queriedEntity = await connection.QuerySingleOrDefaultAsync<TodoDbContract>(
                this.selectByTodoListIdScript,
                new
                {
                    todoListId,
                });

            if (queriedEntity == null)
            {
                throw new EntityNotFoundException<TodoDbContract>($"Could not find '{this.EntityName}' with '{nameof(TodoDbContract.TodoListId)}' '{todoListId}'");
            }

            this.Logger.LogInformation("Successfully executed SELECT for '{EntityName}' with '{AttributeName}' '{TodoListId}'", this.EntityName, nameof(TodoDbContract.TodoListId), queriedEntity.TodoListId);
            return queriedEntity;
        }
    }
}
