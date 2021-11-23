namespace WebApiWithGenerics.WebApi.Contracts.Todo
{
    using System;
    using System.Threading.Tasks;

    using WebApiWithGenerics.WebApi.Contracts.Common;

    public interface ITodoService : ICrudService<Guid, TodoCreateRequest, TodoCreateResponse, TodoGetResponse, TodoUpdateRequest, TodoUpdateResponse, TodoDeleteResponse>
    {
        public Task<TodoGetResponse> GetByTodoListIdAsync(string todoListId);
    }
}
