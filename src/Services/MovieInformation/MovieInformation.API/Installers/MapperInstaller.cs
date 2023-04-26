using AutoMapper;
using MovieInformation.Domain.Models;
using MovieInformation.Infrastructure.ResponseDtos;

namespace MovieInformation.API.Installers;

public static class MapperInstaller
{
    public static IServiceCollection InstallMappings(this IServiceCollection collection)
    {
        collection.AddScoped<IMapper>(_ => new Mapper(CreateMappings()));
        return collection;
    }

    private static MapperConfiguration CreateMappings()
    {
      var mapper = new MapperConfiguration(config =>
          {
              config.CreateMap<Genre, ReadGenreDto>().ReverseMap();
              config.CreateMap<Language, ReadLanguageDto>().ReverseMap();
              config.CreateMap<Keyword, ReadKeywordDto>().ReverseMap();
              config.CreateMap<Movie, ReadDetailedMovieDto>().ReverseMap();
              config.CreateMap<Movie, ReadMovieDto>().ReverseMap();
              config.CreateMap<MovieCollectionPage, ReadMovieCollectionPageDto>().ReverseMap();
              config.CreateMap<MovieCollection, ReadMovieCollectionDto>()
                  .ForMember(destination => destination.Collection,
                      opt => opt.MapFrom(src => src.pages.SelectMany(p => p.Movies).ToList()))
                  .ForMember(destination => destination.MissingPages,
                      options => options.MapFrom(from => from.pages
                          .Where(p => p.IsMissing())
                          .Select(p => p.Page)
                          .ToList()));
          });  
      mapper.AssertConfigurationIsValid();
      return mapper;
    } 
}