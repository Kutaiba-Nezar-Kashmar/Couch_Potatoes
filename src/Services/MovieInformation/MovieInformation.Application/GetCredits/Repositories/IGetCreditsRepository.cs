using MovieInformation.Domain.Models;

namespace MovieInformation.Application.GetCredits.Repositories;

public interface IGetCreditsRepository
{
    Task<PersonMovieCredits> GetMovieCredits(int movieId);
}