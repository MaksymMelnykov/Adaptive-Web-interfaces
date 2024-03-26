using DocumentFormat.OpenXml.Bibliography;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;

namespace Lab7
{
    public class CustomHealthCheckResponseWriter
    {
        public static Task WriteResponse(HttpContext context, HealthReport healthReport)
        {
            context.Response.ContentType = "application/json";

            var json = JsonConvert.SerializeObject(new
            {
                statusApplication = healthReport.Status.ToString(),
                healthChecks = healthReport.Entries.Select(x => new
                {
                    x.Key,
                    x.Value.Status,
                    x.Value.Description
                }),
                totalDuration = healthReport.TotalDuration
            },
            Formatting.Indented);

            return context.Response.WriteAsync(json);
        }
    }
}
