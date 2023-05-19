using AutoMapper;
using User.API.Dtos;
using User.Domain;

namespace User.API.Installers;

public static class MappingsInstaller
{
    private class VoteDirectionToStringTypeConverter : ITypeConverter<VoteDirection, string>
    {
        public string Convert(VoteDirection source, string destination, ResolutionContext context)
        {
            return source.ToString();
        }
    }

    private class StringToVoteDirectionTypeConverter : ITypeConverter<string, VoteDirection>
    {
        public VoteDirection Convert(string source, VoteDirection destination, ResolutionContext context)
        {
            return source.ToVoteDirection();
        }
    }

    public static IServiceCollection InstallMappings(this IServiceCollection services)
    {
        MapperConfiguration config = new(cfg =>
        {
            cfg.CreateMap<VoteDto, Vote>().ReverseMap();
            cfg.CreateMap<ReadReviewDto, Review>().ReverseMap();
        });

        services.AddScoped<IMapper>(_ => new Mapper(config));

        return services;
    }
}