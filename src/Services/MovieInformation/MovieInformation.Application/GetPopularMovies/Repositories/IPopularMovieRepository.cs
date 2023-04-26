using System.Text.Json.Nodes;
using MovieInformation.Domain.Models;

namespace MovieInformation.Application.GetPopularMovies.Repositories;

public interface IPopularMovieRepository
{
    Task<MovieCollectionPage> GetPopularMovies(int page);
}