using Microsoft.Extensions.DependencyInjection;
using Person.Application.FetchPersonDetails;
using Person.Application.FetchPersonMovieCredits;

namespace Person.Application;
public static class PersonServiceCollection
{
    public static IServiceCollection InstallPersonServices(
        this IServiceCollection services)
    {
        services.InstallPersonDetailsServices();
        services.InstallFetchPersonMovieCreditsServices();
        return services;
    }
}
