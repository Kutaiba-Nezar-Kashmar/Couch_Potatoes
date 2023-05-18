using Metrics.Domain.Models.Person;

namespace Metrics.Application.PersonMetrics.Calculations;

public interface ICalculatePersonStatistics
{
    int CalculateNumberOfMovies(PersonMovieCredits credits);
    float CalculateAverageMovieRatingAsCast(PersonMovieCredits credits);
    float CalculateAverageMovieRatingAsCrew(PersonMovieCredits credits);
    int CalculateKnownForGenre(PersonMovieCredits credits);
}