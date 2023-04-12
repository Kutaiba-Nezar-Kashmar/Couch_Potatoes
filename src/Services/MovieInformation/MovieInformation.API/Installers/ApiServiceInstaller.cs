using MovieInformation.Domain.Repositories;
using MovieInformation.Domain.Services;
using MovieInformation.Infrastructure.Repositories;

namespace MovieInformation.API.Installers;

public static class ApiServiceInstaller
{
    public static IServiceCollection Install(IServiceCollection collection)
    {
        collection.AddControllers();
        collection.AddSwaggerGen();
        collection.AddScoped<IMovieRepository, TmdbMovieRepository>();
        collection.AddScoped<IMovieService, MovieService>();
        return  collection;
    }
}