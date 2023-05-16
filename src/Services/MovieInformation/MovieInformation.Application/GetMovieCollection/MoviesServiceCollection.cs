using Microsoft.Extensions.DependencyInjection;
using MovieInformation.Application.GetMovieCollection.Repositories;

namespace MovieInformation.Application.GetMovieCollection;

public static class MoviesServiceCollection
{
    public static IServiceCollection InstallMovieCollectionServices(
        this IServiceCollection collections)
    {
        collections
            .AddScoped<IMovieCollectionRepository, MovieCollectionRepository>();
        return collections;
    }
}