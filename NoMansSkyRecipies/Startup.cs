using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using NoMansSkyRecipies.Configuration;
using NoMansSkyRecipies.CQRS.PipelineBehaviors;
using NoMansSkyRecipies.CustomSettings;
using NoMansSkyRecipies.Middleware;
using Serilog;
using IApplicationLifetime = Microsoft.Extensions.Hosting.IApplicationLifetime;

namespace NoMansSkyRecipies
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var jwtSettings = new JwtSettings();
            this.Configuration.Bind(nameof(JwtSettings), jwtSettings);

            
            services.AddVersionedApiExplorer();

            services.ConfigureInstances(jwtSettings);

            services.AddMemoryCache();

            services.AddRouting();

            services.AddMediatR(typeof(Startup));
            services.AddValidatorsFromAssembly(typeof(Startup).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.ConfigureRepositories();

            services.ConfigureLocalization();

            services.AddControllers()
                .AddDataAnnotationsLocalization()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.Formatting = Formatting.Indented;
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Ignore;
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                })
                .AddXmlSerializerFormatters()
                .AddXmlDataContractSerializerFormatters();

            ////auth
            services.ConfigureAuthentication(jwtSettings);

            ////ÁÄ
            services.ConfigureDbContext(jwtSettings, this.Configuration);

            ////Âåðñ³¿
            services.ConfigureVersions();

            ////swagger
            services.ConfigureSwagger();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider, IHostApplicationLifetime applicationLifetime)
        {
            applicationLifetime.ApplicationStopping.Register(this.OnClosing);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger(options => { options.RouteTemplate = "api-docs/{documentName}/docs.json"; });
                app.UseSwaggerUI(c =>
                {
                    //c.SwaggerEndpoint("/swagger/v1/swagger.json", "Recipes v1");
                    c.RoutePrefix = "api-docs";
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        c.SwaggerEndpoint($"{description.GroupName}/docs.json", description.GroupName);
                    }
                });
            }

            app.Use(async (context, next) =>
            {
                context.Request.EnableBuffering();
                await next();
            });

            app.UseRequestLoggingMidleware();

            app.UseApiExceptionHandlerMiddleware();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseRequestLocalization();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void OnClosing()
        {
            Log.Logger.Information("Exit");
            Log.CloseAndFlush();
        }
    }
}