using MovieInformation.Domain.Models;

namespace MovieInformation.Application.GetTopRatedMovies.Repositories;

public interface ITopRatedMoviesRepository
{
    public Task<MovieCollection> GetTopRatedMovies();
}