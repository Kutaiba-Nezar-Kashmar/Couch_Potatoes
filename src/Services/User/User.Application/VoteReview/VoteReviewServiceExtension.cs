using Microsoft.Extensions.DependencyInjection;
using User.Application.VoteReview.Repository;

namespace User.Application.UpvoteReview;

public static class VoteReviewServiceExtension
{
    public static IServiceCollection InstallVoteReviewServices(this IServiceCollection services)
    {
        services.AddScoped<IVoteReviewRepository, VoteReviewRepository>();
        return services;
    }
}