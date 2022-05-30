using System;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NoMansSkyRecipies.CustomSettings;

namespace NoMansSkyRecipies.Configuration
{
    public static class AuthenticationConfiguration
    {
        public static void ConfigureAuthentication(this IServiceCollection services, JwtSettings jwtSettings)
        {
            services.AddAuthentication()
                .AddCookie(x =>
                {
                    x.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                    x.SlidingExpiration = true;
                    x.AccessDeniedPath = "/Forbidden";
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = true;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = jwtSettings.ValidateIssuer,
                        ValidateAudience = jwtSettings.ValidateAudience,
                        ValidateLifetime = jwtSettings.ValidateLifetime,
                        ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                        ////необхідно для того, щоб токен, термін дії якого сплив, не спрацював
                        ClockSkew = TimeSpan.Zero
                    };
                });
        }

    }
}
