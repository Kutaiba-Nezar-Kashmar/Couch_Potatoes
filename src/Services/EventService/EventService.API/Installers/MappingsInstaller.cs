using AutoMapper;
using EventService.API.Dtos;
using EventService.Domain;

namespace EventService.API.Installers;

public static class MappingsInstaller
{
    public class SchemaPropertyTypeToStringConverter : ITypeConverter<SchemaPropertyType, string>
    {
        public string Convert(SchemaPropertyType source, string destination, ResolutionContext context)
        {
            return source.ToString();
        }
    }

    public class StringToSchemaPropertyTypeConverter : ITypeConverter<string, SchemaPropertyType>
    {
        public SchemaPropertyType Convert(string source, SchemaPropertyType destination, ResolutionContext context)
        {
            return source.ToSchemaPropertyType();
        }
    }

    public static IServiceCollection InstallMappings(this IServiceCollection collection)
    {
        MapperConfiguration config = new(cfg =>
        {
            cfg.CreateMap<SchemaProperty, SchemaPropertyDto>().ReverseMap();

            cfg.CreateMap<ContentSchema, ContentSchemaDto>().ReverseMap();

            cfg.CreateMap<EventSchema, EventSchemaDto>().ReverseMap();
        });


        collection.AddScoped<IMapper>(_ => new Mapper(config));
        return collection;
    }
}