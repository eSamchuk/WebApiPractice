using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace WeaponSystemsAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

            Log.Logger = new LoggerConfiguration().WriteTo.Logger(x =>
                {
                    x.WriteTo.Debug(LogEventLevel.Information, outputTemplate: "[{Timestamp:HH:mm:ss}] [{Level:u3}] {Message:l} {NewLine}");
                })
            .CreateLogger();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>().UseIISIntegration();
                });
    }
}
