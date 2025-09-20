using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using System.Reflection;
using TaskManager.Application.Contracts.Repository;
using TaskManager.Application.Mapping;
using TaskManager.Infrastructure.Repository;

namespace TaskManager.API.Extensions
{
    public static class ContainerExtensions
    {
        public static void AddControllers(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
        }

        public static void AddSwagger(this WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, securityScheme: new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Description = "Enter the Bearer Authorization string as following: `Bearer` Generated-JWT-Token",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });

                options.AddSecurityRequirement(
                        new OpenApiSecurityRequirement()
                        {
                            {
                                new OpenApiSecurityScheme()
                                {
                                    Reference = new OpenApiReference()
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = JwtBearerDefaults.AuthenticationScheme
                                    }
                                },
                                new string[]{}
                            }
                        }
                );


                #region XML documentation
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
                #endregion

            });
        }

        public static void AddRepository(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ITodoRepository, TodoRepository>();
        }

        public static void AddMapster(this WebApplicationBuilder builder)
        {
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(typeof(MappingConfig).Assembly);

            builder.Services.AddSingleton(config);
            builder.Services.AddScoped<IMapper, ServiceMapper>();
        }
    }
}
