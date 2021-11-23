namespace WebApiWithGenerics.WebApi.Contracts.Todo
{
    using System;

    public class TodoUpdateResponse : ITodoWithId
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Guid TodoListId { get; set; }

        public string Status { get; set; }

        public DateTime Deadline { get; set; }
    }
}
