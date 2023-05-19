namespace User.API.Installers;

public static class MiddlewareInstaller
{
    public static IServiceCollection InstallMiddlewares(this IServiceCollection services)
    {
        services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddMediatR(config => config.RegisterServicesFromAssemblies(
            AppDomain.CurrentDomain.Load("User.Application")));

        return services;
    }
}