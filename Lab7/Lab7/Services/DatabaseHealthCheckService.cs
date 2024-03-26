using Lab7.Database;
using Lab7.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Lab7.Services
{
    public class DatabaseHealthCheckService : IHealthCheck
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;

        public DatabaseHealthCheckService(IDbContextFactory<AppDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                using var dbContext = _dbContextFactory.CreateDbContext();
                await dbContext.Database.CanConnectAsync(cancellationToken);
                return HealthCheckResult.Healthy("Database is healthy");
            }
            catch (Exception ex)
            {
                return new HealthCheckResult(
                    status: HealthStatus.Unhealthy,
                    description: "Database is not available",
                    exception: ex);
            }
        }
    }
}
