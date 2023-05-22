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
                PosterPath = c.PosterPath,
                Title = c.Title,
                MovieId = c.MovieId,
                ReleaseDate = c.ReleaseDate,
                Character = c.Character
            }).ToList(),
            CreditsAsCrew = from.CreditsAsCrew.Select(c => new CrewDto
            {
                PosterPath = c.PosterPath,
                Title = c.Title,
                Department = c.Department,
                Job = c.Job,
                MovieId = c.MovieId,
                ReleaseDate = c.ReleaseDate
            }).ToList()
        };
    }
}