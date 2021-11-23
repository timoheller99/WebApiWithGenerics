namespace WebApiWithGenerics.WebApi.Extensions
{
    using System;

    using AutoMapper;
    using AutoMapper.Configuration;

    using Dapper;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using Serilog;

    using WebApiWithGenerics.WebApi.Contracts.User;
    using WebApiWithGenerics.WebApi.Repositories;
    using WebApiWithGenerics.WebApi.Services;
    using WebApiWithGenerics.WebApi.SqlScriptGenerators;
    using WebApiWithGenerics.WebApi.Validation;

    using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

    /// <summary>
    ///     Extension class for custom implementations.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSerilogLogging(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddLogging(c => c.ClearProviders());

            services.AddSerilogServices(configuration);

            return services;
        }

        public static IServiceCollection AddMappings(this IServiceCollection services)
        {
            var mappingConfigurationExpression = new MapperConfigurationExpression()
                .AddUserMappings();

            var mapper = new MapperConfiguration(mappingConfigurationExpression).CreateMapper();

            services.AddSingleton(mapper);

            return services;
        }

        public static IServiceCollection AddDapperGuidMapper(this IServiceCollection services)
        {
            SqlMapper.AddTypeHandler(new SqlGuidTypeHandler());
            SqlMapper.RemoveTypeMap(typeof(Guid));
            SqlMapper.RemoveTypeMap(typeof(Guid?));

            return services;
        }

        public static IServiceCollection AddMySqlScriptGenerators(this IServiceCollection services)
        {
            services.AddSingleton<UserDapperMySqlScriptGenerator>();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();

            return services;
        }

        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddSingleton<UserValidator>();

            return services;
        }

        public static IServiceCollection AddValidationServices(this IServiceCollection services)
        {
            services.AddSingleton<ValidationService<UserValidator, IUser>>();

            return services;
        }

        /// <summary>
        ///     Adds a <see cref="Microsoft.Extensions.Logging.ILogger" /> to the IOC.
        /// </summary>
        /// <param name="services">
        ///     <see cref="IServiceCollection" />
        /// </param>
        /// <param name="configuration">
        ///     <see cref="IConfiguration" />
        /// </param>
        private static void AddSerilogServices(this IServiceCollection services, IConfiguration configuration)
        {
            var loggingConfig = new LoggerConfiguration().ReadFrom.Configuration(configuration);

            services.AddLogging(builder => builder.AddSerilog());

            Log.Logger = loggingConfig.CreateLogger();
            AppDomain.CurrentDomain.ProcessExit += (s, e) => Log.CloseAndFlush();

            services.AddSingleton(Log.Logger);
        }
    }
}
