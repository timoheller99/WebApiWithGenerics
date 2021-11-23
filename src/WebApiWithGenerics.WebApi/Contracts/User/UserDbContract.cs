namespace WebApiWithGenerics.WebApi.Contracts.User
{
    using System;
    using System.ComponentModel;

    using WebApiWithGenerics.WebApi.Contracts.Common;

    [DisplayName("User")]
    public class UserDbContract : DbContract<UserDbContract>, IUserWithId
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }
    }
}
