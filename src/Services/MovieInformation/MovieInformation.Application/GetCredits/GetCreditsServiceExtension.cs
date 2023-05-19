using Microsoft.Extensions.DependencyInjection;
using MovieInformation.Application.GetCredits.Repositories;
using MovieInformation.Application.GetMovie.Repositories;

namespace MovieInformation.Application.GetCredits;

public static class GetCreditsServiceExtension
{
    public static IServiceCollection InstallGetCreditsServices(this IServiceCollection services)
    {
        services.AddScoped<IGetCreditsRepository, GetCreditsRepository>();
        return services;
    }
}