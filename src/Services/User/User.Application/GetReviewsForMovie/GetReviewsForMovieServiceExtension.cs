using Microsoft.Extensions.DependencyInjection;
using User.Application.GetReviewsForMovie.Repository;

namespace User.Application.GetReviewsForMovie;

public static class GetReviewsForMovieServiceExtension
{
    public static IServiceCollection InstallGetReviewsForMoviesServices(this IServiceCollection services)
    {
        services.AddScoped<IGetReviewsForMovieRepository, GetReviewsForMovieRepository>();
        return services;
    }
}