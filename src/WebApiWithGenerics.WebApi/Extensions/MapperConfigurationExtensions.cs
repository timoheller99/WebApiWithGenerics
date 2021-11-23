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

        public static MapperConfigurationExpression AddTodoMappings(this MapperConfigurationExpression expression)
        {
            expression.CreateMap<TodoCreateRequest, TodoDbContract>();
            expression.CreateMap<TodoDbContract, TodoCreateResponse>();

            expression.CreateMap<TodoDbContract, TodoGetResponse>();

            expression.CreateMap<TodoUpdateRequest, TodoDbContract>();
            expression.CreateMap<TodoDbContract, TodoUpdateResponse>();

            expression.CreateMap<TodoDbContract, TodoDeleteResponse>();

            return expression;
        }
    }
}
