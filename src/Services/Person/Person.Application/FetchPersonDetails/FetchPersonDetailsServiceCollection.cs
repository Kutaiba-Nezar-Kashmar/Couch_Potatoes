using Microsoft.Extensions.DependencyInjection;
using Person.Application.FetchPersonDetails.Repositories;

namespace Person.Application.FetchPersonDetails;

public static class FetchPersonDetailsServiceCollection
{
    public static IServiceCollection InstallPersonDetailsServices(
        this IServiceCollection services)
    {
        services
            .AddScoped<IFetchPersonDetailsRepository,
                FetchPersonDetailsRepository>();
        return services;
    }
}