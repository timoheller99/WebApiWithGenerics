namespace WebApiWithGenerics.WebApi.CustomExceptions
{
    using System;

    public class EntityNotFoundException<TDbContract> : Exception
    {
        public EntityNotFoundException()
        {
        }

        public EntityNotFoundException(string message)
            : base(message)
        {
        }

        public EntityNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public EntityNotFoundException(string message, Exception inner, Guid id)
            : base(message, inner)
        {
            this.Id = id;
        }

        public Guid Id { get; set; }
    }
}
