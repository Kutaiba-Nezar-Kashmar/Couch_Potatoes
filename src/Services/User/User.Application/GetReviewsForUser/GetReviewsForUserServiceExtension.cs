using Microsoft.Extensions.DependencyInjection;
using User.Application.GetReviewsForUser.Repository;

namespace User.Application.GetReviewsForUser;

public static class GetReviewsForUserServiceExtension
{
    public static IServiceCollection InstallGetReviewsForUserServices(this IServiceCollection services)
    {
        services.AddScoped<IGetReviewsForUserRepository, GetReviewsForUserRepository>();
        return services;
    }
}