using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NoMansSkyRecipies.CustomSettings;
using NoMansSkyRecipies.Data;
using UsersData;

namespace NoMansSkyRecipies.Configuration
{
    public static class DbContextConfiguration
    {
        public static void ConfigureDbContext(this IServiceCollection services, JwtSettings jwtSettings,
            IConfiguration configuration)
        {
            services.AddDbContext<UserDataDbContext>(x =>
                x.UseSqlServer(configuration.GetConnectionString("AuthData")), ServiceLifetime.Transient);

            services.AddDbContext<RecipiesDbContext>(x =>
                x.UseSqlServer(configuration.GetConnectionString("RecipiesDb")), ServiceLifetime.Transient);
        }
    }
}
