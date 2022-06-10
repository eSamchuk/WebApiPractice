using Microsoft.AspNetCore.Http;

namespace NoMansSkyRecipies.Tracing
{
    public static class AspNetInstrumentationExtensions
    {
        public static bool FilterRequests(HttpContext context)
        {
            return context.Request.Path != "/metrics-text";
        }
    }
}
