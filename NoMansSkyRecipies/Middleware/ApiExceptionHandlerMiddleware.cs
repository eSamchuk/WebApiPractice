using System;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ILogger = Serilog.ILogger;

namespace NoMansSkyRecipies.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ApiExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ApiExceptionHandlerMiddleware(RequestDelegate next, ILogger logger)
        {
            this._next = next;
            this._logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                await this.HandleExceptionAsync(httpContext, e);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.Error("Internal server error: {message}; inner exception : {inner}", exception.Message, (exception.InnerException?.Message ?? "none"));

            ProblemDetails details = new ProblemDetails
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                Title = "Internal Server Error",
                Status = (int)HttpStatusCode.InternalServerError,
                Instance = context.Request.Path,
            };

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(details));
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ApiExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseApiExceptionHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ApiExceptionHandlerMiddleware>();
        }
    }
}
