using Microsoft.Extensions.DependencyInjection;
using Search.Application.MultiSearch;

namespace Search.Application;
public static class SearchServiceCollection
{
    public static IServiceCollection InstallSearchServices(
        this IServiceCollection services)
    {
        services.InstallMultiSearchServices();
        return services;
    }
}
