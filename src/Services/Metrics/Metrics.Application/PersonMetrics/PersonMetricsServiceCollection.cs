using Metrics.Application.PersonMetrics.Math;
using Metrics.Application.PersonMetrics.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Metrics.Application.PersonMetrics;

public static class PersonMetricsServiceCollection
{
    public static IServiceCollection InstallPersonServiceCollection(
        this IServiceCollection services)
    {
        services
            .AddScoped<IFetchPersonMovieCreditsRepository,
                FetchPersonMovieCreditsRepository>();
        services
            .AddScoped<ICalculatePersonStatistics, CalculatePersonStatistics>();
        return services;
    }
}