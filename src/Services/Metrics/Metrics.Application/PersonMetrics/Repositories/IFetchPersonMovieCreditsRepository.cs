using Metrics.Domain.Models.Person;

namespace Metrics.Application.PersonMetrics.Repositories;

public interface IFetchPersonMovieCreditsRepository
{
    Task<PersonMovieCredits> FetchPersonMovieCreditsByPersonId(int personId);
}