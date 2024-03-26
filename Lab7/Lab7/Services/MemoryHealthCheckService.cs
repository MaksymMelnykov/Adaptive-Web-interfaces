using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Lab7.Services
{
    public class MemoryHealthCheckService : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var availableMemory = GC.GetTotalMemory(false);
            if (availableMemory < 1024 * 1024 * 100)
            {
                return Task.FromResult(new HealthCheckResult(
                    status: HealthStatus.Unhealthy,
                    description: "Available memory is low"));
            }

            return Task.FromResult(HealthCheckResult.Healthy("Memory is healthy"));
        }
    }
}
