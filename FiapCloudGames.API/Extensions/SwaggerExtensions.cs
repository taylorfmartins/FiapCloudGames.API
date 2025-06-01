using Microsoft.OpenApi.Models;
using System.Reflection;

namespace FiapCloudGames.API.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "FIAP Cloud Games API",
                    Version = "v1",
                    Description = "API da Plataforma de Games da FIAP",
                    Contact = new OpenApiContact
                    {
                        Name = "Taylor Figueira Martins",
                        Email = "taylorfmartins@gmail.com",
                        Url = new Uri("https://www.linkedin.com/in/taylorfmartins/")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT",
                        Url = new Uri("https://opensource.org/license/mit")
                    }
                });

                // XML de Documentação Gerado e adicionado ao Redoc
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                var coreXmlFile = "FiapCloudGames.Core.xml";
                var coreXmlPath = Path.Combine(AppContext.BaseDirectory, coreXmlFile);
                if (File.Exists(coreXmlPath))
                {
                    c.IncludeXmlComments(coreXmlPath);
                }

                var applicationXmlFile = "FiapCloudGames.Application.xml";
                var applicationXmlPath = Path.Combine(AppContext.BaseDirectory, applicationXmlFile);
                if (File.Exists(applicationXmlPath))
                {
                    c.IncludeXmlComments(applicationXmlPath);
                }

                var repositoryXmlFile = "FiapCloudGames.Application.xml";
                var repositoryXmlPath = Path.Combine(AppContext.BaseDirectory, repositoryXmlFile);
                if (File.Exists(repositoryXmlPath))
                {
                    c.IncludeXmlComments(repositoryXmlPath);
                }

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Por favor, insira 'Bearer' [espaço] e o token JWT",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseReDoc(c =>
            {
                c.DocumentTitle = "FIAP Cloud Games - Documentation";
                c.SpecUrl = "/swagger/v1/swagger.json";
            });

            return app;
        }
    }
}