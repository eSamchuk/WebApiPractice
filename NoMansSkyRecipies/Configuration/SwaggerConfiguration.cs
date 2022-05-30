using System.IO;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using NoMansSkyRecipies.Middleware;

namespace NoMansSkyRecipies.Configuration
{
    public static class SwaggerConfiguration
    {
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(x =>
            {
                var securitySchema = new OpenApiSecurityScheme()
                {
                    Description = "Use JWT bearer to authenticate",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    Reference = new OpenApiReference()
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                x.AddSecurityDefinition("Bearer", securitySchema);
                x.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {securitySchema, new[] { "Bearer" }}
                });

                x.ResolveConflictingActions(descriptions => descriptions.First());
                x.IncludeXmlComments(Path.Combine(System.AppContext.BaseDirectory, "ApiDoc.xml"));
                x.OperationFilter<SwaggerLocalizationHeader>();
            });

        }
    }
}
