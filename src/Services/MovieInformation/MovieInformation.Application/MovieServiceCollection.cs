using Microsoft.Extensions.DependencyInjection;
using MovieInformation.Application.GetCredits;
using MovieInformation.Application.GetMovie;
using MovieInformation.Application.GetMovieCollection;

namespace MovieInformation.Application;

public static class MovieServiceCollection
{
    public static IServiceCollection InstallMovieServices(
        this IServiceCollection services)
    {
        services.InstallMovieCollectionServices();
        services.InstallGetMovieServices();
        services.InstallGetCreditsServices();
        
        return services;
    }
}