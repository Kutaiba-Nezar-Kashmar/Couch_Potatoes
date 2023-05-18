using Microsoft.Extensions.DependencyInjection;
using User.Application.DeleteReview.Repository;

namespace User.Application.DeleteReview;

public static class DeleteReviewForMovieServiceExtension
{
    public static IServiceCollection InstallDeleteReviewForMovieServices(this IServiceCollection services)
    {
        services.AddScoped<IDeleteReviewForMovieRepository, DeleteReviewForMovieRepository>();
        return services;
    }
}