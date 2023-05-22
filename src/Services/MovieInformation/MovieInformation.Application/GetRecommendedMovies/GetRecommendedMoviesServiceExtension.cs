using Microsoft.Extensions.DependencyInjection;
using MovieInformation.Application.GetMovieCollection.Repositories;
using MovieInformation.Application.GetRecommendedMovies.Repositories;

namespace MovieInformation.Application.GetRecommendedMovies;

public static class GetRecommendedMoviesServiceExtension
{
    public static IServiceCollection InstallRecommendedMoviesServices(
        this IServiceCollection collections)
    {
        collections
            .AddScoped<IGetRecommendedMoviesRepository, GetRecommendedMoviesRepository>();
        return collections;
    }
}