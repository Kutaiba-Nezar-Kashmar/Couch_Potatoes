using Person.Domain.Models.Person;
using Person.Infrastructure.ApiDtos;

namespace Person.Infrastructure.Util.Mappers;

public class
    DomainToPersonDetailsDtoMapper : IDtoToDomainMapper<PersonDetails,
        PersonDetailsDto>
{
    public PersonDetailsDto Map(PersonDetails from)
    {
        return new PersonDetailsDto
        {
            Birthday = from.Birthday,
            Biography = from.Biography,
            Aliases = from.Aliases,
            ProfilePath = from.ProfilePath,
            KnownForDepartment = from.KnownForDepartment,
            Name = from.Name,
            PlaceOfBirth = from.PlaceOfBirth,
            Popularity = from.Popularity,
            DeathDay = from.DeathDay,
            Homepage = from.Homepage,
            Gender = Enum.GetName(from.Gender),
            IsAdult = from.IsAdult
        };
    }
}