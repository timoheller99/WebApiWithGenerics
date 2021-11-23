namespace WebApiWithGenerics.WebApi.Contracts.User
{
    using System;

    public class UserDeleteResponse : IUserWithId
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }
    }
}
