using System.Text.Json;
using System.Text.Json.Nodes;
using MovieInformation.Domain.Models;

namespace MovieInformation.Application.GetPopularMovies.Repositories;

public class PopularMovieRepository : IPopularMovieRepository
{
    // TODO: Make this configurable at runtime
    private const string Uri = "https://api.themoviedb.org/3/movie";
    private string _apiKey = Environment.GetEnvironmentVariable("TMDB_API_KEY");
    private HttpClient _httpClient = new();

    public async Task<IReadOnlyCollection<Movie>> GetPopularMovies(int skip, int numberOfPages)
    {
        var res = await _httpClient.GetAsync($"{Uri}/popular?api_key={_apiKey}");

        if (!res.IsSuccessStatusCode)
        {
            throw new Exception("FAILED TO FETCH");
        }

        var contentString = await res.Content.ReadAsStringAsync();
        
        return JsonObject.Parse(contentString).Deserialize<IReadOnlyCollection<Movie>>();
    }
}