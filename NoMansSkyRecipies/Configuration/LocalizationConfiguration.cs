using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;

namespace NoMansSkyRecipies.Configuration
{
    public static class LocalizationConfiguration
    {
        public static void ConfigureLocalization(this IServiceCollection services)
        {
            ////локалізація
            services.Configure<RequestLocalizationOptions>(opt =>
            {
                var supportedCultures = new List<CultureInfo>
                    { new CultureInfo("uk-UA"), new CultureInfo("en-US") };

                opt.DefaultRequestCulture = new RequestCulture(new CultureInfo("en-US"));
                opt.SupportedCultures = supportedCultures;
                opt.SupportedUICultures = supportedCultures;
                opt.RequestCultureProviders = new[] { new AcceptLanguageHeaderRequestCultureProvider() };
            });
        }
    }
}
