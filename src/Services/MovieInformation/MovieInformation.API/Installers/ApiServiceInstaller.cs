using AutoMapper;
using MovieInformation.Application;
using MovieInformation.Domain.Models;
using MovieInformation.Infrastructure.ResponseDtos;

namespace MovieInformation.API.Installers;

public static class ApiServiceInstaller
{
    public static IServiceCollection InstallControllers(this IServiceCollection collection)
    {
        collection.AddControllers();
        collection.InstallMovieServices();
        return collection;
    }

}