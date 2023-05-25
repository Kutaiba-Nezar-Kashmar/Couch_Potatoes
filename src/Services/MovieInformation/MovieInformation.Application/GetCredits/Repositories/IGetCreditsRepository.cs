using MovieInformation.Domain.Models;
using MovieInformation.Domain.Models.Person;

namespace MovieInformation.Application.GetCredits.Repositories;

public interface IGetCreditsRepository
{
    Task<PersonMovieCredits> GetMovieCredits(int movieId);
}