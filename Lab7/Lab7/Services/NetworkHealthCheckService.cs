using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Lab7.Services
{
    public class NetworkHealthCheckService : IHealthCheck
    {
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            using var httpClient = new HttpClient();
            try
            {
                await httpClient.GetAsync("https://moodle3.chmnu.edu.ua/", cancellationToken);
            }
            catch (Exception ex)
            {
                return new HealthCheckResult(
                    status: HealthStatus.Unhealthy,
                    description: "Network is not available",
                    exception: ex);
            }

            return HealthCheckResult.Healthy("Network is healthy");
        }
    }
}
