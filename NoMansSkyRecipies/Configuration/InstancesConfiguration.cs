using Microsoft.AspNetCore.Routing.Internal;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NoMansSkyRecipies.CustomSettings;
using NoMansSkyRecipies.Services;
using NoMansSkyRecipies.Services.SwaggerVersions;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace NoMansSkyRecipies.Configuration
{
    public static class InstancesConfiguration
    {
        public static void ConfigureInstances(this IServiceCollection services, JwtSettings jwtSettings)
        {
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSingleton(jwtSettings);
            services.AddScoped<IAuthService, AuthService>();
            services.AddSingleton(Log.Logger);
        }
    }
}
