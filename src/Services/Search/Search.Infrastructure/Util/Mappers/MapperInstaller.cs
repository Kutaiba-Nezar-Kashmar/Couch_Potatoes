using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Search.Domain.Models;
using Search.Infrastructure.TmdbDtos.Movie;
using Search.Infrastructure.TmdbDtos.MultiSearch;
using Search.Infrastructure.TmdbDtos.Person;

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
                .ForAllMembers(opts =>
                    opts.Condition((src, dest, srcMember) =>
                        srcMember != null));
            ;
            config.CreateMap<TmdbMultiSearchResultDto, PersonSearch>()
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