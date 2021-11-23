namespace WebApiWithGenerics.WebApi.Contracts.User
{
    using System;
    using System.Threading.Tasks;

    using WebApiWithGenerics.WebApi.Contracts.Common;

    public interface IUserRepository : ICrudRepository<Guid, UserDbContract>
    {
        public Task<UserDbContract> GetByEmailAsync(string email);
    }
}
