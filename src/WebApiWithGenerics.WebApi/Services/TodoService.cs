namespace WebApiWithGenerics.WebApi.Services
{
    using System.Threading.Tasks;

    using AutoMapper;

    using WebApiWithGenerics.WebApi.Contracts.Todo;

    public class TodoService :
        CrudService<ITodoRepository, TodoDbContract, TodoCreateRequest, TodoCreateResponse, TodoGetResponse, TodoUpdateRequest, TodoUpdateResponse, TodoDeleteResponse>,
        ITodoService
    {
        public TodoService(IMapper mapper, ITodoRepository repository)
            : base(mapper, repository)
        {
        }

        public async Task<TodoGetResponse> GetByTodoListIdAsync(string todoListId)
        {
            var entity = await this.Repository.GetByTodoListIdAsync(todoListId);

            return this.Mapper.Map<TodoGetResponse>(entity);
        }
    }
}
