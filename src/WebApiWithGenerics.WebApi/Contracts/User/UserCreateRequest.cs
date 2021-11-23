namespace WebApiWithGenerics.WebApi.Contracts.User
{
    public class UserCreateRequest : IUser
    {
        public string Username { get; set; }

        public string Email { get; set; }
    }
}
