using Microsoft.Extensions.DependencyInjection;
using User.Application.UpdateReviewForMovie.Exceptions;
using User.Application.UpdateReviewForMovie.Repository;

namespace User.Application.UpdateReviewForMovie;

public static class UpdateReviewForMovieServiceExtension
{
    public static IServiceCollection InstallUpdateReviewForMovieServices(this IServiceCollection services)
    {
        services.AddScoped<IUpdateReviewForMovieRepository, UpdateReviewForMovieRepository>();
        return services;
    }
}