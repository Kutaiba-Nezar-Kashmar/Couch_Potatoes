using Microsoft.Extensions.DependencyInjection;
using User.Application.CreateReviewForMovie.Repository;

namespace User.Application.CreateReviewForMovie;

public static class CreateReviewForMovieServiceExtension
{
    public static IServiceCollection InstallCreateReviewForMovieServices(this IServiceCollection services)
    {
        services.AddScoped<ICreateReviewForMovieRepository, CreateReviewForMovieRepository>();
        return services;
    }
}