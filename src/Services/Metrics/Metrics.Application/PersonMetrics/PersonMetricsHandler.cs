using MediatR;
using Metrics.Application.PersonMetrics.Calculations;
using Metrics.Application.PersonMetrics.Repositories;
using Metrics.Domain.Models.Person;
using Microsoft.Extensions.Logging;

namespace Metrics.Application.PersonMetrics;

public record PersonMetricHandlerRequest
    (int PersonId) : IRequest<PersonStatistics>;

public class PersonMetricsHandler : IRequestHandler<PersonMetricHandlerRequest,
    PersonStatistics>
{
    private readonly IFetchPersonMovieCreditsRepository
        _fetchPersonMovieCreditsRepository;

    private readonly ICalculatePersonStatistics _calculatePersonStatistics;
    private readonly ILogger<PersonMetricsHandler> _logger;

    public PersonMetricsHandler
    (
        IFetchPersonMovieCreditsRepository fetchPersonMovieCreditsRepository,
        ICalculatePersonStatistics calculatePersonStatistics,
        ILogger<PersonMetricsHandler> logger
    )
    {
        _fetchPersonMovieCreditsRepository = fetchPersonMovieCreditsRepository;
        _calculatePersonStatistics = calculatePersonStatistics;
        _logger = logger;
    }

    public async Task<PersonStatistics> Handle
    (
        PersonMetricHandlerRequest request,
        CancellationToken cancellationToken
    )
    {
        var credits =
            await _fetchPersonMovieCreditsRepository
                .FetchPersonMovieCreditsByPersonId(request.PersonId);
        var statistics = PersonStats(credits);
        return statistics;
    }

    private PersonStatistics PersonStats(PersonMovieCredits credits)
    {
        var totalNumberOfMovies =
            _calculatePersonStatistics.CalculateNumberOfMovies(credits);
        var castAverage =
            _calculatePersonStatistics.CalculateAverageMovieRatingAsCast(
                credits);
        var crewAverage =
            _calculatePersonStatistics.CalculateAverageMovieRatingAsCrew(
                credits);
        var knownFor =
            _calculatePersonStatistics.CalculateKnownForGenre(credits);

        return new PersonStatistics
        {
            NumberOfMovies = totalNumberOfMovies,
            AverageMoviesRatingsAsACast = castAverage,
            AverageMoviesRatingsAsACrew = crewAverage,
            KnownForGenreId = knownFor
        };
    }
}