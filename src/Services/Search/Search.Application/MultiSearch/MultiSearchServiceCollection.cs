using Microsoft.Extensions.DependencyInjection;

namespace Search.Application.MultiSearch;

public static class MultiSearchServiceCollection
{
    public static IServiceCollection InstallMultiSearchServices(
        this IServiceCollection services)
    {
        return services;
    }
}