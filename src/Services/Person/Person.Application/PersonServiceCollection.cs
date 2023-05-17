using Microsoft.Extensions.DependencyInjection;
using Person.Application.FetchPersonDetails;

namespace Person.Application;
public static class PersonServiceCollection
{
    public static IServiceCollection InstallPersonServices(
        this IServiceCollection services)
    {
        services.InstallPersonDetailsServices();
        return services;
    }
}
