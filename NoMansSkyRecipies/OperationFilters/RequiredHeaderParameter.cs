using System.Collections.Generic;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace NoMansSkyRecipies.OperationFilters
{
    public class RequiredHeaderParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters ??= new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter()
            {
                  In = ParameterLocation.Header,
                  Name = "Accept",
                  Schema = new OpenApiSchema()
                  {
                      Type = "string",
                      Default = new OpenApiString("application/json, appliction/xml")
                  }
            });

        }
    }
}
