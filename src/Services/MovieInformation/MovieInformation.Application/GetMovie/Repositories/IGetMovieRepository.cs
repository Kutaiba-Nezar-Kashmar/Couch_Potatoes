using MovieInformation.Domain.Models;

namespace MovieInformation.Application.GetMovie.Repositories;

public interface IGetMovieRepository
{
    Task<Movie> GetMovie(int movieId);
}