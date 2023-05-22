using Microsoft.Extensions.DependencyInjection;
using Person.Application.FetchPersonMovieCredits.Repositories;

namespace Person.Application.FetchPersonMovieCredits;

public static class FetchPersonMovieCreditsServiceCollection
{
    public static IServiceCollection InstallFetchPersonMovieCreditsServices(
        this IServiceCollection services)
    {
        services
            .AddScoped<IFetchPersonMovieCreditsRepository,
                FetchPersonMovieCreditsRepository>();
        return services;
    }
}