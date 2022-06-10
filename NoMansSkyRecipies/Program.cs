using System;
using System.Diagnostics;
using App.Metrics.AspNetCore;
using App.Metrics.Formatters.Prometheus;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.File;
using Serilog.Sinks.MSSqlServer;

namespace NoMansSkyRecipies
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Serilog.Debugging.SelfLog.Enable(x => Debug.WriteLine($"SelfLog - {x}"));

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var logDb = configuration.GetConnectionString("LogsDb");

            //Log.Logger = new LoggerConfiguration().WriteTo.Logger(x =>
            //    {
            //        x.WriteTo.File("D:/log_file.txt", LogEventLevel.Information, rollingInterval: RollingInterval.Day, flushToDiskInterval: TimeSpan.FromSeconds(1), outputTemplate: "[{Timestamp:HH:mm:ss}] [{Level:u3}] {Message:l} {NewLine}");
            //        x.WriteTo.Debug(LogEventLevel.Information, outputTemplate: "[{Timestamp:HH:mm:ss}] [{Level:u3}] {Message:l} {NewLine}");
            //        x.Filter.ByIncludingOnly(z => z.Level == LogEventLevel.Information).WriteTo.MSSqlServer(logDb, new MSSqlServerSinkOptions()
            //        {
            //            AutoCreateSqlTable = false,
            //            BatchPostingLimit = 1,
            //            TableName = "NmsRecipesLogs_Info"
            //        });

            //        x.Filter.ByIncludingOnly(z => z.Level == LogEventLevel.Error || z.Level == LogEventLevel.Fatal).WriteTo.MSSqlServer(logDb, new MSSqlServerSinkOptions()
            //        {
            //            AutoCreateSqlTable = false,
            //            BatchPostingLimit = 1,
            //            TableName = "NmsRecipesLogs_Error"
            //        });
            //    })
            //.CreateLogger();

            //var config = new ConfigurationBuilder()
            //    .AddJsonFile("appsettings.json")
            //    .Build();
            //var logDb = config.GetConnectionString("LogsDb");

            //Log.Logger = new LoggerConfiguration()
            //    .MinimumLevel.Information()
            //    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            //    .WriteTo.Debug(LogEventLevel.Information)
            //    .WriteTo.File(path: "D:/file.txt", rollingInterval: RollingInterval.Day, outputTemplate: "[{Timestamp:HH:mm:ss}] [{Level:u3}] {Message:l} {NewLine}")
            //    .Filter.ByIncludingOnly(x => x.Level == LogEventLevel.Information).WriteTo
            //    .MSSqlServer(logDb, new MSSqlServerSinkOptions() { AutoCreateSqlTable = false, TableName = "NmsRecipesLogs_Info", BatchPostingLimit = 1 })
            //    .Filter.ByIncludingOnly(x => x.Level == LogEventLevel.Error).WriteTo
            //    .MSSqlServer(logDb, new MSSqlServerSinkOptions() { AutoCreateSqlTable = false, TableName = "NmsRecipesLogs_Errors", BatchPostingLimit = 1 })
            //    .CreateLogger();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .UseMetricsWebTracking()
                .UseMetrics(opt =>
                {
                    opt.EndpointOptions = endpointOPtions =>
                    {
                        endpointOPtions.MetricsTextEndpointOutputFormatter = new MetricsPrometheusTextOutputFormatter();
                        endpointOPtions.MetricsOutputFormatters =
                            new[] { new MetricsPrometheusProtobufOutputFormatter() };
                        endpointOPtions.EnvironmentInfoEndpointEnabled = false;
                    };
                })
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>().UseIISIntegration(); })
                .UseSerilog(
                (context, configuration) =>
                {
                    configuration.ReadFrom.Configuration(context.Configuration);
                    configuration.Enrich.FromLogContext().Enrich.WithMachineName();
                });
        }
    }
}
