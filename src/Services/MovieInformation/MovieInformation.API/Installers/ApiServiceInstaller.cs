using MovieInformation.Application;

namespace MovieInformation.API.Installers;

public static class ApiServiceInstaller
{
    public static IServiceCollection InstallControllers(this IServiceCollection collection)
    {
        collection.AddControllers();
        collection.InstallMovieServices();
        return  collection;
    }
}