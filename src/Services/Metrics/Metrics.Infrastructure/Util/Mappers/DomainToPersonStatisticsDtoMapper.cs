using Metrics.Domain.Models.Person;
using Metrics.Infrastructure.ControllerDtos;

namespace Metrics.Infrastructure.Util.Mappers;

public class
    DomainToPersonStatisticsDtoMapper : IDtoToDomainMapper<PersonStatistics,
        PersonStatisticsDto>
{
    public PersonStatisticsDto Map(PersonStatistics from)
    {
        return new PersonStatisticsDto
        {
            KnownForGenre = from.KnownForGenre,
            NumberOfMovies = from.NumberOfMovies,
            AverageMoviesRatingsAsACast = from.AverageMoviesRatingsAsACast,
            AverageMoviesRatingsAsACrew = from.AverageMoviesRatingsAsACrew
        };
    }
}