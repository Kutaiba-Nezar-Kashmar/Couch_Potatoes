using Person.Domain.Models.Person;
using Person.Infrastructure.TmdbDtos.PersonDetailsDto;

namespace Person.Infrastructure.Util.Mappers;

public class
    TmdbPersonDetailsDtoToDomainMapper : IDtoToDomainMapper<TmdbPersonDetailsDto
        , PersonDetails>
{
    public PersonDetails Map(TmdbPersonDetailsDto from)
    {
        return new PersonDetails
        {
            IsAdult = from.Adult,
            Aliases = from.AlsoKnownAs,
            Biography = from.Biography,
            Birthday = DateTimeParser.ParseDateTime(from.Birthday),
            DeathDay = DateTimeParser.ParseDateTime(from.DeathDay),
            Gender = (Gender) from.Gender,
            Homepage = from.Homepage,
            Id = from.Id,
            ImdbId = from.ImdbId,
            KnownForDepartment = from.KnownForDepartment,
            Name = from.Name,
            PlaceOfBirth = from.PlaceOfBirth,
            Popularity = from.Popularity,
            ProfilePath = from.ProfilePath
        };
    }
}