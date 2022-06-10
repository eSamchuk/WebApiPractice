using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace NoMansSkyRecipies.Tracing
{
    public static class Enrichments
    {
        public static void TelemetryBasicEntrich(Activity activity, string eventName, object rawObject)
        {
            activity.SetTag("ActivityKind", Enum.GetName(typeof(ActivityKind), activity.Kind));
            activity.SetTag("ActivityParentId", activity.ParentId);
            activity.SetTag("Id", activity.Id);

            if (rawObject is HttpRequest httpRequest)
            {
                activity.SetTag("requestProtocol", httpRequest.Protocol);
                activity.SetTag("ContentType", httpRequest.ContentType);
            }

            if (rawObject is HttpResponse httpResponse)
            {
                activity.SetTag("responseLength", httpResponse.ContentLength);
                activity.SetTag("ContentType", httpResponse.ContentType);
            }
        }
    }
}
