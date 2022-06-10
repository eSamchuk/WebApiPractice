using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace NoMansSkyRecipies.Middleware
{
    public class SwaggerLocalizationHeader : IOperationFilter
    {
        private readonly IServiceProvider _serviceProvider;

        public SwaggerLocalizationHeader(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters ??= new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter()
            {
                Name = "Accept-Language",
                Description = "Language of API call response",
                In = ParameterLocation.Header,
                Required = false,
                Schema = new OpenApiSchema()
                {
                    Type = nameof(String),
                    Enum = (_serviceProvider
                            .GetService(typeof(IOptions<RequestLocalizationOptions>)) as IOptions<RequestLocalizationOptions>)?
                        .Value?
                        .SupportedCultures?.Select(c => new OpenApiString(c.TwoLetterISOLanguageName)).ToList<IOpenApiAny>(),
                }
            });
        }
    }
}
