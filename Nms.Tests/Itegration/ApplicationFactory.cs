using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using NoMansSkyRecipies;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NoMansSkyRecipies.Data;
using UsersData;

namespace Nms.Tests.Itegration
{
    public class ApplicationFactory : WebApplicationFactory<Program>
    {

        public ApplicationFactory() : base()
        {
        }

        private IHost? _host;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var dbDescriptors = services
                    .Where(x => x.ServiceType == typeof(RecipiesDbContext) |
                                x.ServiceType == typeof(UserDataDbContext)).ToList();

                if (dbDescriptors.Any())
                {
                    foreach (var dbDescriptor in dbDescriptors)
                    {
                        services.Remove(dbDescriptor);
                    }
                }

                services.AddDbContext<RecipiesDbContext>(opt =>
                {
                    opt.UseInMemoryDatabase("InMemoryRecipesDb");
                });


                services.AddDbContext<UserDataDbContext>(opt =>
                {
                    opt.UseInMemoryDatabase("InMemoryUsersDb");
                });


                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                using var appContext = scope.ServiceProvider.GetRequiredService<RecipiesDbContext>();
                try
                {
                    appContext.Database.EnsureDeleted();
                    appContext.Database.EnsureCreated();
                }
                catch (Exception ex)
                {
                    throw;
                }

                using var userContext = scope.ServiceProvider.GetRequiredService<UserDataDbContext>();
                try
                {
                    userContext.Database.EnsureCreated();
                }
                catch (Exception ex)
                {
                    throw;
                }

            });
        }

        public async Task Initialize()
        {
            var client = this.CreateClient();
            var startupTimeout = TimeSpan.FromMilliseconds(2000);
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            do
            {
                var response = await client.GetAsync("Resources");
                if (response.IsSuccessStatusCode)
                {
                    return;
                }
            } while (stopWatch.Elapsed < startupTimeout);

            throw new InvalidOperationException("Cannot initialize service within the expected timeout");
        }

        public async ValueTask DisposeAsync()
        {
            if (this._host != null)
            {
                await this._host.StopAsync();
            }

            base.Dispose();
        }
    }
}
