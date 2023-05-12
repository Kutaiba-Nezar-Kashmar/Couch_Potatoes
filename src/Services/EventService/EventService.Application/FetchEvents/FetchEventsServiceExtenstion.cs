using EventService.Application.FetchEvents.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace EventService.Application.FetchEvents;

public static class FetchEventsServiceExtenstion
{
    public static IServiceCollection InstallFetchEventServices(
        this IServiceCollection services)
    {
        services.AddScoped<IFetchEvents, FetchEventSchema>();
        return services;
    }
}