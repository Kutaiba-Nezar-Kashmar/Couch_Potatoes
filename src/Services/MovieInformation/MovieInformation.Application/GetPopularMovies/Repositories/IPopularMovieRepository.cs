using System.Text.Json.Nodes;
using MovieInformation.Domain.Models;

namespace MovieInformation.Application.GetPopularMovies.Repositories;

public interface IPopularMovieRepository
{
    Task<IReadOnlyCollection<Movie>> GetPopularMovies(int skip, int numberOfPages);
}