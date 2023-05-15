using Microsoft.Extensions.DependencyInjection;

namespace MovieInformation.Application.GetMovie;

public static class GetMovieServiceExtension
{
    public static IServiceCollection InstallGetMovieServices(this IServiceCollection services)
    {
        return services;
    }
}