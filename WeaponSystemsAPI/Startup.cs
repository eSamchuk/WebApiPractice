using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NmsRecipes.DAL.Interfaces;
using NmsRecipes.DAL.Repositories;
using NoMansSkyRecipies.Data;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace WeaponSystemsAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApiVersioning(x => x.DefaultApiVersion = ApiVersion.Default);
            services.AddControllers();

            services.AddTransient<IResourceRepository, ResourcesRepository>();

            services.AddMvc().AddJsonOptions(x =>
            {
                x.JsonSerializerOptions.IgnoreNullValues = true;
                x.JsonSerializerOptions.WriteIndented = true;
            });

            services.AddOpenTelemetryMetrics(config =>
            {
                config.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("SecondLayer"))
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation();
            });

            services.AddOpenTelemetryTracing(builder =>
            {
                builder.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("SecondLayer"))
                    .AddSqlClientInstrumentation(opt =>
                    {
                        opt.EnableConnectionLevelAttributes = true;
                        opt.SetDbStatementForText = true;
                        //opt.Enrich = (activity, eventName, rawObject) =>
                        //{
                        //    activity.SetTag()
                        //});
                    })
                    .AddHttpClientInstrumentation()
                    .AddAspNetCoreInstrumentation(opt =>
                    {
                        opt.Enrich = (activity, eventName, body) =>
                        {
                            activity.SetTag(nameof(activity.ParentId), activity.ParentId);
                        };
                    })
                    .AddZipkinExporter()
                    .AddJaegerExporter();
        });

            services.AddDbContext<RecipiesDbContext>(x =>
            {
                x.UseSqlServer(this.Configuration.GetConnectionString("RecipiesDb"));
            });


        }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
}
