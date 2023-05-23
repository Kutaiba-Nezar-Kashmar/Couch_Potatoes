using Microsoft.Extensions.DependencyInjection;
using Search.Application.MultiSearch.Repositories;

namespace Search.Application.MultiSearch;

public static class MultiSearchServiceCollection
{
    public static IServiceCollection InstallMultiSearchServices(
        this IServiceCollection services)
    {
        services.AddScoped<IMultiSearchRepository, MultiSearchRepository>();
        return services;
    }
}