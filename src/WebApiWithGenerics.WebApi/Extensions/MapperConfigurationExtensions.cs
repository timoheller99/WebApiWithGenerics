namespace WebApiWithGenerics.WebApi.Extensions
{
    using AutoMapper.Configuration;

    using WebApiWithGenerics.WebApi.Contracts.Todo;
    using WebApiWithGenerics.WebApi.Contracts.User;

    public static class MapperConfigurationExtensions
    {
        public static MapperConfigurationExpression AddUserMappings(this MapperConfigurationExpression expression)
        {
            expression.CreateMap<UserCreateRequest, UserDbContract>();
            expression.CreateMap<UserDbContract, UserCreateResponse>();

            expression.CreateMap<UserDbContract, UserGetResponse>();

            expression.CreateMap<UserUpdateRequest, UserDbContract>();
            expression.CreateMap<UserDbContract, UserUpdateResponse>();

            expression.CreateMap<UserDbContract, UserDeleteResponse>();

            return expression;
        }
    }
}
