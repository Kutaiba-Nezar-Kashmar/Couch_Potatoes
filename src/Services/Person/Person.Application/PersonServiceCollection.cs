using Microsoft.Extensions.DependencyInjection;

namespace Person.Application;
public static class PersonServiceCollection
{
    public static IServiceCollection InstallPersonServices(
        this IServiceCollection services)
    {
        return services;
    }
}
