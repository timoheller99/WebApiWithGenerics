namespace WebApiWithGenerics.WebApi.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Dapper;

    using Microsoft.Extensions.Logging;

    using MySqlConnector;

    using WebApiWithGenerics.WebApi.Configuration;
    using WebApiWithGenerics.WebApi.Contracts.Common;
    using WebApiWithGenerics.WebApi.CustomExceptions;
    using WebApiWithGenerics.WebApi.SqlScriptGenerators;

    public abstract class DapperMySqlCrudRepository<TDbContract> : ICrudRepository<Guid, TDbContract>
        where TDbContract : IWithId
    {
        private readonly string deleteScript;

        private readonly string insertScript;

        private readonly string selectAllScript;

        private readonly string selectByIdScript;

        private readonly string updateScript;

        protected DapperMySqlCrudRepository(
            ILogger<DapperMySqlCrudRepository<TDbContract>> logger,
            DatabaseConfiguration options,
            DapperMySqlScriptGenerator<TDbContract> dapperMySqlScriptGenerator)
        {
            this.Logger = logger;
            this.ConnectionString = options.ConnectionString;

            this.EntityName = DbContract<TDbContract>.GetEntityName();
            this.insertScript = dapperMySqlScriptGenerator.GenerateInsertScript();
            this.selectAllScript = dapperMySqlScriptGenerator.GenerateSelectAllScript();
            this.selectByIdScript = dapperMySqlScriptGenerator.GenerateSelectByIdScript();
            this.updateScript = dapperMySqlScriptGenerator.GenerateUpdateScript();
            this.deleteScript = dapperMySqlScriptGenerator.GenerateDeleteScript();
        }

        protected string EntityName { get; }

        protected string ConnectionString { get; }

        protected ILogger<DapperMySqlCrudRepository<TDbContract>> Logger { get; }

        public async Task<TDbContract> CreateAsync(TDbContract entity)
        {
            await using var connection = new MySqlConnection(this.ConnectionString);
            await connection.ExecuteAsync(this.insertScript, entity);

            var createdEntry = await this.GetByIdAsync(entity.Id);

            this.Logger.LogInformation("Successfully executed INSERT for '{EntityName}' with ID '{Id}'", this.EntityName, createdEntry.Id.ToString());
            return createdEntry;
        }

        public async Task<IEnumerable<TDbContract>> GetAllAsync()
        {
            await using var connection = new MySqlConnection(this.ConnectionString);
            var entries = await connection.QueryAsync<TDbContract>(this.selectAllScript);

            this.Logger.LogInformation("Successfully executed SELECT all for '{EntityName}'", this.EntityName);
            return entries;
        }

        public async Task<TDbContract> GetByIdAsync(Guid id)
        {
            await using var connection = new MySqlConnection(this.ConnectionString);
            var queriedEntity = await connection.QuerySingleOrDefaultAsync<TDbContract>(
                this.selectByIdScript,
                new
                {
                    id,
                });

            if (queriedEntity == null)
            {
                throw new EntityNotFoundException<TDbContract>($"Could not find '{this.EntityName}' with ID '{id}'");
            }

            this.Logger.LogInformation("Successfully executed SELECT for '{EntityName}' with ID '{Id}'", this.EntityName, queriedEntity.Id.ToString());
            return queriedEntity;
        }

        public async Task<TDbContract> UpdateAsync(TDbContract entity)
        {
            await using var connection = new MySqlConnection(this.ConnectionString);
            await connection.ExecuteAsync(this.updateScript, entity);

            var updatedEntry = await this.GetByIdAsync(entity.Id);

            this.Logger.LogInformation("Successfully executed UPDATE for '{EntityName}' with ID '{Id}'", this.EntityName, updatedEntry.Id.ToString());
            return updatedEntry;
        }

        public async Task<TDbContract> DeleteAsync(Guid id)
        {
            await using var connection = new MySqlConnection(this.ConnectionString);
            var deletedEntity = await this.GetByIdAsync(id);

            await connection.ExecuteAsync(
                this.deleteScript,
                new
                {
                    id,
                });

            this.Logger.LogInformation("Successfully executed DELETE for '{EntityName}' with ID '{Id}'", this.EntityName, deletedEntity.Id.ToString());
            return deletedEntity;
        }
    }
}
