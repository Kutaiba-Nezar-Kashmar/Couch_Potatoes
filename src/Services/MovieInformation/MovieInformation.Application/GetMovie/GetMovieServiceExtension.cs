using Microsoft.Extensions.DependencyInjection;
using MovieInformation.Application.GetMovie.Repositories;

namespace MovieInformation.Application.GetMovie;

public static class GetMovieServiceExtension
{
    public static IServiceCollection InstallGetMovieServices(this IServiceCollection services)
    {
        services.AddScoped<IGetMovieRepository, GetMovieRepository>();
        return services;
    }
}