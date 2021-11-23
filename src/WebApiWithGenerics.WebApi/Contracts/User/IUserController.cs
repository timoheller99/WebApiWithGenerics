namespace WebApiWithGenerics.WebApi.Contracts.User
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using WebApiWithGenerics.WebApi.Contracts.Common;

    public interface IUserController : ICrudController<Guid, UserCreateRequest, UserCreateResponse, UserGetResponse, UserUpdateRequest, UserUpdateResponse, UserDeleteResponse>
    {
        public Task<ActionResult<UserGetResponse>> GetByEmailAsync(string email);
    }
}
