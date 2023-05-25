using AutoMapper;
using MovieInformation.Domain.Models;
using MovieInformation.Domain.Models.MovieImages;
using MovieInformation.Domain.Models.MovieVideos;
using MovieInformation.Infrastructure.ControllerDtos.Images;
using MovieInformation.Infrastructure.ControllerDtos.Movie;
using MovieInformation.Infrastructure.ControllerDtos.Videos;
using MovieInformation.Infrastructure.ResponseDtos;
using MovieInformation.Infrastructure.TmbdDto.ImageDto;
using MovieInformation.Infrastructure.TmbdDto.MovieVideoDto;

namespace MovieInformation.API.Installers;

public static class MapperInstaller
{
    public static IServiceCollection InstallMappings(
        this IServiceCollection collection)
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
            config.CreateMap<Movie, ReadMovieDto>().ReverseMap();
            config.CreateMap<Movie, MovieControllerDto>().ForMember(
                    destination => destination.Posters,
                    opt => opt.MapFrom(src =>
                        src.Posters.Select(p => new MovieImageDto
                        {
                            FilePath = p.FilePath,
                            Height = p.Height,
                            Width = p.Width
                        }).ToList()))
                .ForMember(destination => destination.Videos,
                    opt => opt.MapFrom(src =>
                        src.Videos.Select(v => new MovieVideoDto()
                        {
                            Id = v.Id,
                            Key = v.Key,
                            Name = v.Name,
                            PublishedAt = v.PublishedAt
                        }).ToList()));
            config.CreateMap<TmdbVideoDto, MovieVideo>();
            config.CreateMap<MovieVideo, MovieVideoDto>();
            config.CreateMap<MovieImage, MovieImageDto>();
            config.CreateMap<TmdbBackdropsDto, MovieImage>();
            config.CreateMap<TmdbLogoDto, MovieImage>();
            config.CreateMap<TmdbPosterDto, MovieImage>();
            config.CreateMap<MovieCollection, ReadMovieCollectionDto>()
                .ForMember(destination => destination.Collection,
                    opt => opt.MapFrom(src =>
                        src.pages.SelectMany(p => p.Movies).ToList()))
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