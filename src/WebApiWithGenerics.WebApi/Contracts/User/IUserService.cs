namespace WebApiWithGenerics.WebApi.Contracts.User
{
    using System;
    using System.Threading.Tasks;

    using WebApiWithGenerics.WebApi.Contracts.Common;

    public interface IUserService : ICrudService<Guid, UserCreateRequest, UserCreateResponse, UserGetResponse, UserUpdateRequest, UserUpdateResponse, UserDeleteResponse>
    {
        public Task<UserGetResponse> GetByEmailAsync(string email);
    }
}
