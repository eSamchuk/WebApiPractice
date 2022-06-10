using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ILogger = Serilog.ILogger;

namespace NoMansSkyRecipies.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger logger)
        {
            this._next = next;
            this._logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Method == "POST")
            {
                var request = httpContext.Request;
                string content = string.Empty;

                using (var sr = new StreamReader(request.Body, Encoding.UTF8, true, 1024, true))
                {
                    content = await sr.ReadToEndAsync();
                }

                var user = !string.IsNullOrWhiteSpace(httpContext.User.Identity.Name)
                    ? httpContext.User.Identity.Name
                    : "Anonymous";
                httpContext.Request.Body.Position = 0;
                this._logger.Information(
                    "{5} request was sent to {0}, time was: {1}, remote IP address: {4}  user: {2}  request content is: {3}",
                    request.Path, DateTime.Now, user, content, httpContext.Connection.RemoteIpAddress,
                    httpContext.Request.Method);
            }

            await _next(httpContext);
        }
    }

    public static class RequestLoggingMidlewareExtensions
    {
        public static void UseRequestLoggingMidleware(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<RequestLoggingMiddleware>();
        }
    }
}
