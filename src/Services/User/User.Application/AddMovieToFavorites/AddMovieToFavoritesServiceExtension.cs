using Microsoft.Extensions.DependencyInjection;
using User.Application.AddMovieToFavorites.Repository;

namespace User.Application.AddMovieToFavorites;

public static class AddMovieToFavoritesServiceExtension
{
    public static IServiceCollection InstallAddMovieToFavoritesServices(this IServiceCollection services)
    {
        services.AddScoped<IAddMovieToFavoritesRepository, AddMovieToFavoritesRepository>();
        return services;
    }
}