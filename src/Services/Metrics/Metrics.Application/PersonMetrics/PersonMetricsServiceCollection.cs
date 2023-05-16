using Microsoft.Extensions.DependencyInjection;

namespace Metrics.Application.PersonMetrics;

public static class PersonMetricsServiceCollection
{
    public static IServiceCollection InstallPersonServiceCollection(
        this IServiceCollection services)
    {
        return services;
    }
}