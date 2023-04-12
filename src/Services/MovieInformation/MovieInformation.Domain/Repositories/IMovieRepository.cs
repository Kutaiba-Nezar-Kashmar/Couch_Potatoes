using System.Text.Json.Nodes;

namespace MovieInformation.Domain.Repositories;

public interface IMovieRepository
{
   Task<JsonObject> GetPopularMovies();
}