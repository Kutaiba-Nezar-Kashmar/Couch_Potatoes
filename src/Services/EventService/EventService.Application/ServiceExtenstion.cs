using EventService.Application.FetchEvents;
using Microsoft.Extensions.DependencyInjection;

namespace EventService.Application;
public static class ServiceExtenstion
{
    public static IServiceCollection InstallServices(
        this IServiceCollection services)
    {
        services.InstallFetchEventServices();
        return services;
    }
}
