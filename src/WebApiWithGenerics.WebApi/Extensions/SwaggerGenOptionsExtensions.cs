namespace WebApiWithGenerics.WebApi.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;

    using Swashbuckle.AspNetCore.SwaggerGen;

    public static class SwaggerGenOptionsExtensions
    {
        public static SwaggerGenOptions AddXmlComments(this SwaggerGenOptions options)
        {
            var assembly = Assembly.GetExecutingAssembly();

            // Set the comments path for the Swagger JSON and UI.
            var file = $"{assembly.GetName().Name}.xml";
            var path = Path.Combine(AppContext.BaseDirectory, file);

            if (File.Exists(path))
            {
                options.IncludeXmlComments(path);
            }

            return options;
        }

        public static SwaggerGenOptions AddCustomAuth(this SwaggerGenOptions options)
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = @"Authorization header.<br><br>
                      Enter your token in the text input below.
                      <br><br>Example: '12345abcdef'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer",
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                },
            });

            return options;
        }
    }
}
