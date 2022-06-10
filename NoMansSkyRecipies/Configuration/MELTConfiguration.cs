using Microsoft.Extensions.DependencyInjection;
using NoMansSkyRecipies.Tracing;
using OpenTelemetry.Exporter;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace NoMansSkyRecipies.Configuration
{
    public static class MELTConfiguration
    {
        public static void ConfigureTracingAndMetrics(this IServiceCollection services)
        {
            services.AddOpenTelemetryMetrics(config =>
            {
                config.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("NmsRecipeAPI"))
                    .AddAspNetCoreInstrumentation();
            });

            services.AddOpenTelemetryTracing(builder =>
            {
                builder.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("NmsRecipeAPI"))
                    .AddHttpClientInstrumentation()
                    .AddSqlClientInstrumentation(opt =>
                    {
                        opt.SetDbStatementForText = true;
                        opt.Enrich = (activity, enentName, obj) =>
                        {

                        };
                    })
                    .AddAspNetCoreInstrumentation(opt =>
                    {
                        opt.Filter = AspNetInstrumentationExtensions.FilterRequests;
                        opt.Enrich = Enrichments.TelemetryBasicEntrich;
                    }) 
                    .AddZipkinExporter(opt =>
                    {
                        opt.UseShortTraceIds = false;
                    })
                    .AddJaegerExporter();
            });

        }
    }
}
