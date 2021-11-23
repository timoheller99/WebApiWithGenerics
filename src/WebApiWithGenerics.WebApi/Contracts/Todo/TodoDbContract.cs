namespace WebApiWithGenerics.WebApi.Contracts.Todo
{
    using System;
    using System.ComponentModel;

    using WebApiWithGenerics.WebApi.Contracts.Common;

    [DisplayName("Todo")]
    public class TodoDbContract : DbContract<TodoDbContract>, ITodoWithId
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Guid TodoListId { get; set; }

        public string Status { get; set; }

        public DateTime Deadline { get; set; }
    }
}
