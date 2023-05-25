using MovieInformation.Domain.Models;
using MovieInformation.Domain.Models.Movie;

namespace MovieInformation.Application.GetRecommendedMovies.Repositories;

public interface IGetRecommendedMoviesRepository
{
    Task<MovieCollectionPage> GetRecommendedMovies(int page,int movieId);
}