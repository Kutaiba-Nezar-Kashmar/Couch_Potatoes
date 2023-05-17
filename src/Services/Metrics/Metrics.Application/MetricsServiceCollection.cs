using Metrics.Application.PersonMetrics;
using Microsoft.Extensions.DependencyInjection;

namespace Metrics.Application;
public static class MetricsServiceCollection
{
    public static IServiceCollection InstallMetricsServices(
        this IServiceCollection services)
    {
        services.InstallPersonServiceCollection();
        return services;
    }
}
