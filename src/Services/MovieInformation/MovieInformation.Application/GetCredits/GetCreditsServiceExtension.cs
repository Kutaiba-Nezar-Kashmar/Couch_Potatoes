using Microsoft.Extensions.DependencyInjection;
using MovieInformation.Application.GetCredits.Repositories;

namespace MovieInformation.Application.GetCredits;

public static class GetCreditsServiceExtension
{
    public static IServiceCollection InstallGetCreditsServices(this IServiceCollection services)
    {
        services.AddScoped<IGetCreditsRepository, GetCreditsRepository>();
        return services;
    }
}