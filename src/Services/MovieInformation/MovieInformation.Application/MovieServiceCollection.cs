using Microsoft.Extensions.DependencyInjection;
using MovieInformation.Application.GetMovie;
using MovieInformation.Application.GetPopularMovies;

namespace MovieInformation.Application;

public static class MovieServiceCollection
{
    public static IServiceCollection InstallMovieServices(
        this IServiceCollection services)
    {
        services.InstallPopularMovieServices();
        services.InstallGetMovieServices();
        
        return services;
    }
}