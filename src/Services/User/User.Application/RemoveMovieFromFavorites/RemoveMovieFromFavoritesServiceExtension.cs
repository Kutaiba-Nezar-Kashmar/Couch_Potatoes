using Microsoft.Extensions.DependencyInjection;
using User.Application.RemoveMovieFromFavorites.Repository;

namespace User.Application.RemoveMovieFromFavorites;

public static class RemoveMovieFromFavoritesServiceExtension
{
    public static IServiceCollection InstallRemoveMovieFromFavoritesServices(this IServiceCollection services)
    {
        services.AddScoped<IRemoveMovieFromFavoritesRepository, RemoveMovieFromFavoritesRepository>();
        return services;
    }
}