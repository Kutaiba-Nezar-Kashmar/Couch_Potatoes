using Person.Domain.Models.Person;

namespace Person.Application.FetchPersonMovieCredits.Repositories;

public interface IFetchPersonMovieCreditsRepository
{
    Task<PersonMovieCredits> FetchPersonMovieCreditsByPersonId(int personId);
}