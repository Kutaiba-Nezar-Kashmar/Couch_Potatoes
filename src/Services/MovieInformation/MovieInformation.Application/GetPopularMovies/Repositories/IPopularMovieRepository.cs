using System.Text.Json.Nodes;

namespace MovieInformation.Application.GetPopularMovies.Repositories;

public interface IPopularMovieRepository
{
   Task<JsonObject> GetPopularMovies();
}