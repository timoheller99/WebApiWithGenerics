namespace WebApiWithGenerics.WebApi
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;

    using WebApiWithGenerics.WebApi.Configuration;
    using WebApiWithGenerics.WebApi.Extensions;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSerilogLogging(this.Configuration);

            var openApiInfo = new OpenApiInfo();
            this.Configuration.GetSection("OpenApiInfo").Bind(openApiInfo);
            services.AddSingleton(openApiInfo);

            var databaseConfiguration = new DatabaseConfiguration
            {
                ConnectionString = this.Configuration.GetConnectionString("Default"),
            };
            services.AddSingleton(databaseConfiguration);

            services.AddMappings();
            services.AddDapperGuidMapper();
            services.AddMySqlScriptGenerators();
            services.AddRepositories();
            services.AddServices();
            services.AddValidators();
            services.AddValidationServices();

            services.AddControllers();

            services.AddSwaggerGen(swaggerGenOptions =>
            {
                swaggerGenOptions.SwaggerDoc(openApiInfo.Version, openApiInfo);

                swaggerGenOptions = swaggerGenOptions.AddXmlComments();

                swaggerGenOptions = swaggerGenOptions.AddCustomAuth();

                swaggerGenOptions.CustomSchemaIds(x => x.Name);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApiWithGenerics.WebApi v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
