using Metrics.Domain.Models.Person;

namespace Metrics.Application.PersonMetrics.Math;

public interface ICalculatePersonStatistics
{
    PersonStatistics CalculateStatistics(PersonMovieCredits credits);
}