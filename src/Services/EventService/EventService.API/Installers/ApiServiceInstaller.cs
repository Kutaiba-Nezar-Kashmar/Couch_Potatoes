namespace EventService.API.Installers;

public static class ApiServiceInstaller
{
    public static IServiceCollection InstallControllers(this IServiceCollection collection)
    {
        collection.AddControllers();
        return collection;
    }

}