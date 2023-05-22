using Microsoft.Extensions.DependencyInjection;
using User.Application.AddMovieToFavorites;
using User.Application.CreateReviewForMovie;
using User.Application.DeleteReview;
using User.Application.GetReviewsForMovie;
using User.Application.GetReviewsForUser;
using User.Application.RemoveMovieFromFavorites;
using User.Application.UpdateReviewForMovie;
using User.Application.UpvoteReview;
using User.Infrastructure;

namespace User.Application;

public static class RootServiceInstaller
{
    public static IServiceCollection InstallUserServiceDependencies(this IServiceCollection services)
    {
        services.InstallGetReviewsForMoviesServices();
        services.InstallCreateReviewForMovieServices();
        services.InstallAddMovieToFavoritesServices();
        services.InstallRemoveMovieFromFavoritesServices();
        services.InstallVoteReviewServices();
        services.InstallUpdateReviewForMovieServices();
        services.InstallDeleteReviewForMovieServices();
        services.InstallGetReviewsForUserServices();
        
        services.InstallGetUsersServices();
        services.AddScoped<IAuthenticationRepository, FirebaseAuthRepository>();
        return services;
    }
}