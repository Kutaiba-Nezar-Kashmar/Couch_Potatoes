using System.Text.Json.Nodes;

namespace MovieInformation.Application.GetPopularMovies;

public class GetPopularMoviesDto
{
    public JsonObject Data{ get; set; }
}