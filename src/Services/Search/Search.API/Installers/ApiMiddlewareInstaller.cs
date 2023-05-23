namespace Search.API.Installers;

public static class ApiMiddlewareInstaller
{
    public static IServiceCollection InstallMiddlewareServices(
        this IServiceCollection services)
    {
        services.AddSwaggerGen();
        services.AddMediatR(config =>
            config.RegisterServicesFromAssemblies(
                AppDomain.CurrentDomain.Load("Search.Application")));
        return services;
    }
}