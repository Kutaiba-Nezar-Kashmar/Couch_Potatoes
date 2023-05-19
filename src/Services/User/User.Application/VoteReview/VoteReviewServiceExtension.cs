using Microsoft.Extensions.DependencyInjection;
using User.Application.UpvoteReview.Repository;

namespace User.Application.UpvoteReview;

public static class VoteReviewServiceExtension
{
    public static IServiceCollection InstallVoteReviewServices(this IServiceCollection services)
    {
        services.AddScoped<IVoteReviewRepository, VoteReviewReviewRepository>();
        return services;
    }
}