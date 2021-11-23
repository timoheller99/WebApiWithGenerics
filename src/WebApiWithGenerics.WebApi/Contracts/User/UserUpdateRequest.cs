namespace WebApiWithGenerics.WebApi.Contracts.User
{
    public class UserUpdateRequest : IUser
    {
        public string Username { get; set; }

        public string Email { get; set; }
    }
}
