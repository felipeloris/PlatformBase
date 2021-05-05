using Loris.Common.Webapi.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using NSwag;
using NSwag.Generation.Processors;
using NSwag.Generation.Processors.Contexts;
using NSwag.Generation.Processors.Security;
using System.Linq;

namespace Loris.Common.Webapi.Helpers
{
    public static class SwaggerHelper
    {
        #region Swashbuckle.AspNetCore
        /*
        public static AddSwaggerGen(this IServiceCollection services, AppSettings settings)
        {
            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc($"v{settings.About.Version}", SwaggerHelper.OpenApiInfo(settings));
                swagger.AddSecurityDefinition(settings.Swagger.Id, settings.Swagger.OpenApiSecurityScheme);
                swagger.AddSecurityRequirement(settings.Swagger.OpenApiSecurityRequirement);
            });
        }

        public static UseSwaggerUI UseSwaggerUI(this IApplicationBuilder app, AppSettings settings)
        {
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint(
                    $"/swagger/v{settings.About.Version}/swagger.json",
                    $"{settings.About.Title} (v{settings.About.Version})"); 
            });
        }

        public static OpenApiSecurityScheme OpenApiSecurityScheme(AppSettings settings)
        {
            return new OpenApiSecurityScheme()
            {
                Name = settings.Swagger.Name,
                Type = SecuritySchemeType.ApiKey,
                Scheme = settings.Swagger.Scheme,
                BearerFormat = settings.Swagger.BearerFormat,
                In = ParameterLocation.Header,
                Description = settings.Swagger.Description,
            };
        }

        public static OpenApiSecurityRequirement OpenApiSecurityRequirement(AppSettings settings)
        {
            return new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference()
                            {
                                Id = settings.Swagger.Id,
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new string[] {}
                    }
                };
        }

        public static OpenApiInfo OpenApiInfo(AppSettings settings)
        {
            return new OpenApiInfo()
            {
                Version = settings.Version.ToString(),
                Title = settings.About.Title,
                Description = settings.About.Description
            };
        }
        */
        #endregion

        #region NSwag.AspNetCore

        public static void AddOpenApiDocument(this IServiceCollection services, AppSettings settings)
        {
            services.AddOpenApiDocument(configure =>
            {
                configure.Title = settings.About.Title;
                configure.Version = settings.About.Version.ToString();
                configure.Description = settings.About.Description;

                configure.AddSecurity(settings.Swagger.BearerFormat, Enumerable.Empty<string>(), new NSwag.OpenApiSecurityScheme
                {
                    Name = settings.Swagger.Name,
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = settings.Swagger.Description,
                    //BearerFormat = settings.Swagger.BearerFormat,
                    //Scheme = settings.Swagger.Scheme
                });
                configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));

                //configure.OperationProcessors.Add(new AddHeaderOperationProcessor());
            });
        }

        public class AddHeaderOperationProcessor : IOperationProcessor
        {
            public bool Process(OperationProcessorContext context)
            {
                /*
                context.OperationDescription.Operation.Parameters.Add(
                    new OpenApiParameter
                    {
                        Name = "Sample",
                        Kind = OpenApiParameterKind.Header,
                        Type = NJsonSchema.JsonObjectType.String,
                        IsRequired = true,
                        Description = "This is a test header",
                        Default = "{{\"field1\": \"value1\", \"field2\": \"value2\"}}"
                    });
                */

                context.OperationDescription.Operation.Parameters.Add(
                    new OpenApiParameter
                    {
                        Name = "Content-Language", //Accept-Language
                        Kind = OpenApiParameterKind.Header,
                        Type = NJsonSchema.JsonObjectType.String,
                        IsRequired = true,
                        Description = "Define the language of user",
                        Default = "{{\"Content-Language\": \"pt-BR\"}}"
                    });

                return true;
            }
        }

        #endregion
    }
}
