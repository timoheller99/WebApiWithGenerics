namespace WebApiWithGenerics.WebApi.Services
{
    using System.Threading.Tasks;

    using AutoMapper;

    using WebApiWithGenerics.WebApi.Contracts.User;

    public class UserService :
        CrudService<IUserRepository, UserDbContract, UserCreateRequest, UserCreateResponse, UserGetResponse, UserUpdateRequest, UserUpdateResponse, UserDeleteResponse>,
        IUserService
    {
        public UserService(IMapper mapper, IUserRepository repository)
            : base(mapper, repository)
        {
        }

        public async Task<UserGetResponse> GetByEmailAsync(string email)
        {
            var entity = await this.Repository.GetByEmailAsync(email);

            return this.Mapper.Map<UserGetResponse>(entity);
        }
    }
}
