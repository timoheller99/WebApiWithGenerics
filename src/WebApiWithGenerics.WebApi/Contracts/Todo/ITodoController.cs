namespace WebApiWithGenerics.WebApi.Contracts.Todo
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using WebApiWithGenerics.WebApi.Contracts.Common;

    public interface ITodoController : ICrudController<Guid, TodoCreateRequest, TodoCreateResponse, TodoGetResponse, TodoUpdateRequest, TodoUpdateResponse, TodoDeleteResponse>
    {
        public Task<ActionResult<TodoGetResponse>> GetByTodoListIdAsync(string todoListId);
    }
}
