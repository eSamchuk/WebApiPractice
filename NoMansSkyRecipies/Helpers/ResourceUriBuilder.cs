using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using NmsDisplayData;

namespace NoMansSkyRecipies.Helpers
{
    public static class ResourceUriBuilder
    {
        public static string GetResourceUriByAction(this LinkGenerator linkGenerator, HttpContext context, string area, string actionName, int id, string apiVersion)
        {
            return linkGenerator.GetUriByAction(
                context,
                actionName,
                values: new
                {
                    Id = id,
                    area = area,
                    version = apiVersion.ToString()
                });
        }
    }
}
