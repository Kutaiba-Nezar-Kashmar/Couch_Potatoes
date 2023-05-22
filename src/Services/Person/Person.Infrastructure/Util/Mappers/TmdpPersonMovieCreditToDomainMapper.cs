using Person.Domain.Models.Person;
using Person.Infrastructure.Responses.PersonResponseDtos;

namespace Person.Infrastructure.Util.Mappers;

public class TmdpPersonMovieCreditToDomainMapper : IDtoToDomainMapper<
    GetPersonMovieCreditsResponseDto, PersonMovieCredits>
{
    public PersonMovieCredits Map(GetPersonMovieCreditsResponseDto from)
    {
        return new PersonMovieCredits
        {
            CreditsAsCast = from.Cast.Select(c => new Cast
            {
                BackdropPath = c.BackdropPath,
                OriginalTitle = c.OriginalTitle,
                MovieId = c.Id
            }).ToList(),

            CreditsAsCrew = from.Crew.Select(c => new Crew
            {
                BackdropPath = c.BackdropPath,
                OriginalTitle = c.OriginalTitle,
                Department = c.Department,
                Job = c.Job,
                MovieId = c.Id
            }).ToList()
        };
    }
}