using EventService.Application.FetchEvents;
using EventService.Application.RegisterEventSchemas;
using Microsoft.Extensions.DependencyInjection;

namespace EventService.Application;

public static class ServiceExtenstion
{
    public static IServiceCollection InstallServices(
        this IServiceCollection services)
    {
        services.InstallFetchEventServices();
        services.InstallRegisterEventServices();
        return services;
    }
}