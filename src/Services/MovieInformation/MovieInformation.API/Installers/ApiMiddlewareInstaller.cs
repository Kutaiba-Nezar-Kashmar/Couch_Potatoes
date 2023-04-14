namespace MovieInformation.API.Installers;

public static class ApiMiddlewareInstaller
{
    public static IServiceCollection InstallMiddlewareServices(
        this IServiceCollection collection)
    {
        collection.AddSwaggerGen();
        collection.AddMediatR(config =>
            config.RegisterServicesFromAssemblies(
                AppDomain.CurrentDomain.Load("MovieInformation.Application")));
        return collection;
    }
}