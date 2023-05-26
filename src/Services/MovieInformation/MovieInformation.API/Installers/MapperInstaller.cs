using AutoMapper;
using MovieInformation.Domain.Models;
using MovieInformation.Domain.Models.Movie;
using MovieInformation.Domain.Models.MovieImages;
using MovieInformation.Domain.Models.MovieReleaseDates;
using MovieInformation.Domain.Models.MovieVideos;
using MovieInformation.Domain.Models.Person;
using MovieInformation.Infrastructure.ControllerDtos.Images;
using MovieInformation.Infrastructure.ControllerDtos.Movie;
using MovieInformation.Infrastructure.ControllerDtos.Movie.MovieReleaseDates;
using MovieInformation.Infrastructure.ControllerDtos.Movie.ProductionCompanies;
using MovieInformation.Infrastructure.ControllerDtos.Person;
using MovieInformation.Infrastructure.ControllerDtos.Videos;
using MovieInformation.Infrastructure.ResponseDtos;
using MovieInformation.Infrastructure.ResponseDtos.MovieResponses;
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
            config
                .CreateMap<MovieReleaseDatesDetails,
                    MovieReleaseDateDetailsDto>();
            config
                .CreateMap<MovieReleaseDate,
                    MovieReleaseDatesDto>()
                .ForMember(destination => destination.ReleaseDatesDetails,
                    opt => opt.MapFrom(src =>
                        src.ReleaseDatesDetails.Select(d =>
                            new MovieReleaseDateDetailsDto
                            {
                                ReleaseDate = d.ReleaseDate,
                                Type = d.Type,
                                Certification = d.Certification,
                                Note = d.Note
                            }).ToList()));
            config
                .CreateMap<MovieReleaseDateResponse,
                    MovieReleaseDateResponseDto>();
            config.CreateMap<CastMember, CastMemberDto>();
            config.CreateMap<CrewMember, CrewMemberDto>();
            config.CreateMap<PersonMovieCredits, PersonMovieCreditsDto>();
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
                            Width = p.Width,
                            Lang = p.Lang
                        }).ToList()))
                .ForMember(destination => destination.Videos,
                    opt => opt.MapFrom(src =>
                        src.Videos.Select(v => new MovieVideoDto()
                        {
                            Id = v.Id,
                            Key = v.Key,
                            Name = v.Name,
                            PublishedAt = v.PublishedAt
                        }).ToList()))
                .ForMember(destination => destination.ReleaseDates,
                    opt => opt.MapFrom(src =>
                        src.ReleaseDates.Select(r => new MovieReleaseDatesDto()
                        {
                            Lang = r.Lang,
                            ReleaseDatesDetails = r.ReleaseDatesDetails.Select(
                                d =>
                                    new MovieReleaseDateDetailsDto
                                    {
                                        ReleaseDate = d.ReleaseDate,
                                        Type = d.Type,
                                        Certification = d.Certification,
                                        Note = d.Note
                                    }).ToList()
                        }).ToList()))
                .ForMember(destination => destination.ProductionCompanies,
                    opt => opt.MapFrom(src =>
                        src.ProductionCompanies.Select(p =>
                            new ProductionCompaniesDto()
                            {
                                Id = p.Id,
                                Name = p.Name,
                                LogoPath = p.LogoPath,
                                OriginCountry = p.OriginCountry
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