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
                OriginalTitle = c.OriginalTitle,
                MovieId = c.MovieId
            }).ToList(),
            CreditsAsCrew = from.CreditsAsCrew.Select(c => new CrewDto
            {
                BackdropPath = c.BackdropPath,
                OriginalTitle = c.OriginalTitle,
                Department = c.Department,
                Job = c.Job,
                MovieId = c.MovieId
            }).ToList()
        };
    }
}