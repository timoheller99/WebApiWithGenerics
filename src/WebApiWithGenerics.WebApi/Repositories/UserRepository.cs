namespace WebApiWithGenerics.WebApi.Repositories
{
    using System.Threading.Tasks;

    using Dapper;

    using Microsoft.Extensions.Logging;

    using MySqlConnector;

    using WebApiWithGenerics.WebApi.Configuration;
    using WebApiWithGenerics.WebApi.Contracts.User;
    using WebApiWithGenerics.WebApi.CustomExceptions;
    using WebApiWithGenerics.WebApi.SqlScriptGenerators;

    public class UserRepository : DapperMySqlCrudRepository<UserDbContract>, IUserRepository
    {
        private readonly string selectByEmailScript;

        public UserRepository(ILogger<UserRepository> logger, DatabaseConfiguration options, UserDapperMySqlScriptGenerator userDapperMySqlScriptGenerator)
            : base(logger, options, userDapperMySqlScriptGenerator)
        {
            this.selectByEmailScript = userDapperMySqlScriptGenerator.GenerateSelectByEmailScript();
        }

        public async Task<UserDbContract> GetByEmailAsync(string email)
        {
            await using var connection = new MySqlConnection(this.ConnectionString);
            var queriedEntity = await connection.QuerySingleOrDefaultAsync<UserDbContract>(
                this.selectByEmailScript,
                new
                {
                    email,
                });

            if (queriedEntity == null)
            {
                throw new EntityNotFoundException<UserDbContract>($"Could not find '{this.EntityName}' with email '{email}'");
            }

            this.Logger.LogInformation("Successfully executed SELECT for '{EntityName}' with email '{Email}'", this.EntityName, queriedEntity.Email);
            return queriedEntity;
        }
    }
}
