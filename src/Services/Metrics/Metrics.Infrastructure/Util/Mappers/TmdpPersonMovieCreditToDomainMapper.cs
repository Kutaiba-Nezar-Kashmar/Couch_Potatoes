using Metrics.Domain.Models.Person;
using Metrics.Infrastructure.PersonResponseDtos;
using Metrics.Infrastructure.TmdbDtos.PersonDto;

namespace Metrics.Infrastructure.Util.Mappers;

public class TmdpPersonMovieCreditToDomainMapper : IDtoToDomainMapper<GetPersonMovieCreditsResponseDto, PersonMovieCredits>
{
    public PersonMovieCredits Map(GetPersonMovieCreditsResponseDto from)
    {
        return new PersonMovieCredits
        {
            CreditsAsCast = from.Cast.Select(c => new Cast
            {
                GenreIds = c.GenreIds,
                VoteAverage = c.VoteAverage,
                VoteCount = c.VoteCount
            }).ToList(),
            
            CreditsAsCrew = from.Crew.Select(c => new Crew
            {
                GenreIds = c.GenreIds,
                VoteAverage = c.VoteAverage,
                VoteCount = c.VoteCount
            }).ToList()
        };
    }
}