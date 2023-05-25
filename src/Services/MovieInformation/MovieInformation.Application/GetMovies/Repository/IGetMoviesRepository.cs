using MovieInformation.Domain.Models;

namespace MovieInformation.Application.GetMovies.Repository;

public interface IGetMoviesRepository
{
    Task<IReadOnlyCollection<Movie>> GetMovies(IReadOnlyCollection<int> movieIds);
}