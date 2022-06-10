using System;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeaponSystemCaller;

namespace NoMansSkyRecipies.Configuration
{
    public static class HttpClientsConfiguration {
        public static void ConfigureHttpClients(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<IWeaponSystemCallerService, WeaponSystemCallerService>(opt =>
            {
                opt.BaseAddress = new Uri(configuration["WeaponSysytemAPI:Endpoint"]);
                opt.DefaultRequestHeaders.Clear();
                opt.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });
        }
    }
}
