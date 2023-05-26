using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Search.Domain.Models;
using Search.Infrastructure.ControllerDtos;
using Search.Infrastructure.TmdbDtos.MultiSearch;

namespace Search.Infrastructure.Util.Mappers;

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
            config.CreateMap<TmdbMultiSearchResultDto, MovieSearch>()
                .ForMember(d => d.Id,
                    o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Title, o => o.MapFrom(s => s.Title))
                .ForMember(d => d.PosterPath,
                    o => o.MapFrom(s => s.PosterPath))
                .ForMember(d => d.IsAdult,
                    o => o.MapFrom(s => s.IsAdult))
                .ForMember(d => d.BackdropOath,
                    o => o.MapFrom(s => s.BackdropOath))
                .ForMember(d => d.OriginalLanguage,
                    o => o.MapFrom(s => s.OriginalLanguage))
                .ForMember(d => d.OriginalTitle,
                    o => o.MapFrom(s => s.OriginalTitle))
                .ForMember(d => d.Overview,
                    o => o.MapFrom(s => s.Overview))
                .ForMember(d => d.MediaType,
                    o => o.MapFrom(s => s.MediaType))
                .ForMember(d => d.ReleaseDate,
                    o => o.MapFrom(s =>
                        DateTimeParser.ParseDateTime(s.ReleaseDate)))
                .ForMember(d => d.GenreIds,
                    o => o.MapFrom(s => s.GenreIds))
                .ForMember(d => d.Popularity,
                    o => o.MapFrom(s => s.Popularity))
                .ForMember(d => d.HasVideo,
                    o => o.MapFrom(s => s.HasVideo))
                .ForMember(d => d.VoteAverage,
                    o => o.MapFrom(s => s.VoteAverage))
                .ForMember(d => d.VoteCount,
                    o => o.MapFrom(s => s.VoteCount))
                .ForAllMembers(opts =>
                    opts.Condition((src, dest, srcMember) =>
                        srcMember != null));
            ;
            config.CreateMap<TmdbMultiSearchResultDto, PersonSearch>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
                .ForMember(d => d.ProfilePath,
                    o => o.MapFrom(s => s.ProfilePath))
                .ForMember(d => d.KnownForDepartment,
                    o => o.MapFrom(s => s.KnownForDepartment))
                .ForMember(d => d.OriginalName,
                    o => o.MapFrom(s => s.OriginalLanguage))
                .ForMember(d => d.MediaType,
                    o => o.MapFrom(s => s.MediaType))
                .ForMember(d => d.Popularity,
                    o => o.MapFrom(s => s.Popularity))
                .ForMember(d => d.Gender,
                    o => o.MapFrom(s => s.Gender))
                .ForMember(d => d.KnownFor,
                    o => o.MapFrom(s => s.KnownFor))
                .ForAllMembers(opts =>
                    opts.Condition((src, dest, srcMember) =>
                        srcMember != null));
            ;

            config.CreateMap<MovieSearch, MovieSearchDto>()
                .ForMember(d => d.Id,
                    o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Title, o => o.MapFrom(s => s.Title))
                .ForMember(d => d.PosterPath,
                    o => o.MapFrom(s => s.PosterPath))
                .ForMember(d => d.ReleaseDate,
                    o => o.MapFrom(s => s.ReleaseDate))
                .ForAllMembers(opts =>
                    opts.Condition((src, dest, srcMember) =>
                        srcMember != null));
            ;

            config.CreateMap<PersonSearch, PersonSearchDto>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
                .ForMember(d => d.ProfilePath,
                    o => o.MapFrom(s => s.ProfilePath))
                .ForMember(d => d.KnownFor,
                    o => o.MapFrom(s => s.KnownForDepartment))
                .ForAllMembers(opts =>
                    opts.Condition((src, dest, srcMember) =>
                        srcMember != null));
            ;
        });
        mapper.AssertConfigurationIsValid();
        return mapper;
    }
}