namespace WebApiWithGenerics.WebApi.Contracts.Todo
{
    using System;

    public interface ITodo
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public Guid TodoListId { get; set; }

        public string Status { get; set; }

        public DateTime Deadline { get; set; }
    }
}
