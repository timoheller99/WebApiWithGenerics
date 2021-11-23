namespace WebApiWithGenerics.WebApi.Contracts.Todo
{
    using System;
    using System.Threading.Tasks;

    using WebApiWithGenerics.WebApi.Contracts.Common;

    public interface ITodoRepository : ICrudRepository<Guid, TodoDbContract>
    {
        public Task<TodoDbContract> GetByTodoListIdAsync(string todoListId);
    }
}
