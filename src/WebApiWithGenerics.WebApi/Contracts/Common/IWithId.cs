namespace WebApiWithGenerics.WebApi.Contracts.Common
{
    using System;

    public interface IWithId
    {
        public Guid Id { get; set; }
    }
}
