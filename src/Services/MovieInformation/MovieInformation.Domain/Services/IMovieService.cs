using System.Text.Json.Nodes;

namespace MovieInformation.Domain.Services;

public interface IMovieService
{
    Task<JsonObject> GetPopularMovies();
}
