using MovieInformation.Domain.Models;

namespace MovieInformation.Application.GetRecommendedMovies.Repositories;

public interface IGetRecommendedMoviesRepository
{
    Task<MovieCollectionPage> GetRecommendedMovies(int page,int movieId);
}