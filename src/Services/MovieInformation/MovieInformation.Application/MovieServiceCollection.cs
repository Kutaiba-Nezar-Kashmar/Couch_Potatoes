using Microsoft.Extensions.DependencyInjection;
using MovieInformation.Application.GetPopularMovies;

namespace MovieInformation.Application;

public static class MovieServiceCollection
{
    public static IServiceCollection InstallMovieServices(
        this IServiceCollection collections)
    {
        collections.InstallPopularMovieServices();
        return collections;
    }
}