using EventService.Application.RegisterEventSchemas.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace EventService.Application.RegisterEventSchemas;

public static class InstallRegisterEventSchemasServices
{
    public static IServiceCollection InstallRegisterEventServices(
        this IServiceCollection services)
    {
        services.AddScoped<IRegisterEventsRepository, RegisterEventsRepository>();
        return services;
    }
}