using Microsoft.Extensions.DependencyInjection;
using User.Application.CreateReviewForMovie;
using User.Application.GetReviewsForMovie;

namespace User.Application;

public static class RootServiceInstaller
{
    public static IServiceCollection InstallUserServiceDependencies(this IServiceCollection services)
    {
        services.InstallGetReviewsForMoviesServices();
        services.InstallCreateReviewForMovieServices();
        return services;
    }
}