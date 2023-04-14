using Microsoft.Extensions.DependencyInjection;
using MovieInformation.Application.GetPopularMovies.Repositories;

namespace MovieInformation.Application.GetPopularMovies;

public static class popularMoviesServiceCollection
{
    public static IServiceCollection InstallPopularMovieServices(
        this IServiceCollection collections)
    {
        collections
            .AddScoped<IPopularMovieRepository, PopularMovieRepository>();
        return collections;
    }
}