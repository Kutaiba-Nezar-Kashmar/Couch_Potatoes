using Microsoft.Extensions.DependencyInjection;

namespace User.Application;

public static class GetUsersServiceExtension
{
    public static IServiceCollection InstallGetUsersServices(this IServiceCollection services)
    {
        return services;
    }
}