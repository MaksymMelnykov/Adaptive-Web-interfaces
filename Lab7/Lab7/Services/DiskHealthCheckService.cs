using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Lab7.Services
{
    public class DiskHealthCheckService : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var drives = DriveInfo.GetDrives();
            foreach (var drive in drives)
            {
                if (drive.TotalFreeSpace < 1024 * 1024 * 1024)
                {
                    return Task.FromResult(new HealthCheckResult(
                        status: HealthStatus.Unhealthy,
                        description: $"Drive {drive.Name} is running out of space. Total free space - {drive.TotalFreeSpace}"));
                }
            }

            return Task.FromResult(HealthCheckResult.Healthy("Disk space is healthy"));
        }
    }
}
