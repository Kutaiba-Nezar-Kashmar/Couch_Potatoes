using System.Reflection;
using MovieInformation.Domain.Repositories;
using MovieInformation.Infrastructure.Repositories;

namespace MovieInformation.API.Installers;

public static class ApiServiceInstaller
{
    public static IServiceCollection Install(IServiceCollection collection)
    {
        // NOTE: (mibui 2023-04-12) We should probably split these up into separate installers for middleware and services
        collection.AddControllers();
        collection.AddSwaggerGen();
        collection.AddMediatR(config => config.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.Load("MovieInformation.Application")));
        collection.AddScoped<IMovieRepository, TmdbMovieRepository>();
        return  collection;
    }
}