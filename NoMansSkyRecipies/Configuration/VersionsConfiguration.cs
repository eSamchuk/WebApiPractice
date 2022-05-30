using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace NoMansSkyRecipies.Configuration
{
    public static class VersionsConfiguration
    {
        public static void ConfigureVersions(this IServiceCollection services)
        {
            services.AddApiVersioning(opt =>
            {
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.DefaultApiVersion = ApiVersion.Default;
                opt.ReportApiVersions = true;
            });
        }
    }
}
