using Microsoft.Extensions.DependencyInjection;
using MovieInformation.Application.GetMovies.Repository;

namespace MovieInformation.Application.GetMovies;

public static class GetMoviesServiceExtension
{
    public static IServiceCollection InstallGetMoviesServices(this IServiceCollection services)
    {
        services.AddScoped<IGetMoviesRepository, GetMoviesRepository>();
        return services;
    }
}