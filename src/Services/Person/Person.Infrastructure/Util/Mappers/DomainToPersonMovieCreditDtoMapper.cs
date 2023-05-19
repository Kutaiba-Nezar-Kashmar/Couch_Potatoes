using Person.Domain.Models.Person;
using Person.Infrastructure.ApiDtos;

namespace Person.Infrastructure.Util.Mappers;

public class DomainToPersonMovieCreditDtoMapper : IDtoToDomainMapper<
    PersonMovieCredits, PersonMovieCreditsDto>
{
    public PersonMovieCreditsDto Map(PersonMovieCredits from)
    {
        return new PersonMovieCreditsDto
        {
            CreditsAsCast = from.CreditsAsCast.Select(c => new CastDto
            {
                BackdropPath = c.BackdropPath,
                OriginalTitle = c.OriginalTitle
            }).ToList(),
            CreditsAsCrew = from.CreditsAsCrew.Select(c => new CrewDto
            {
                BackdropPath = c.BackdropPath,
                OriginalTitle = c.OriginalTitle,
                Department = c.Department,
                Job = c.Job
            }).ToList()
        };
    }
}