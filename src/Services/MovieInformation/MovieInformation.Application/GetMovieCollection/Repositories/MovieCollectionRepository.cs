using MovieInformation.Domain.Models;
using MovieInformation.Infrastructure.Exceptions;
using MovieInformation.Infrastructure.TmbdDto.ResponseDto;
using MovieInformation.Infrastructure.Util;

namespace MovieInformation.Application.GetMovieCollection.Repositories;

public class MovieCollectionRepository : IMovieCollectionRepository
{
  
    private string _apiKey = Environment.GetEnvironmentVariable("TMDB_API_KEY");
    private HttpClient _httpClient;

    public MovieCollectionRepository(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("HTTP_CLIENT");
    }

    public async Task<MovieCollectionPage> GetMovieCollection
    (int page,
        string collectionType)
    {
        var res = await _httpClient.GetAsync($"{collectionType}?api_key={_apiKey}");

        if (!res.IsSuccessStatusCode)
        {
            throw new HttpException(
                $"{nameof(GetMovieCollection)}: Failed to fetch movie collection of type: {collectionType}, with status code: {res.StatusCode}");
        }

        var contentString = await res.Content.ReadAsStringAsync();
        var dto = JsonDeserializer.Deserialize<GetMovieCollectionResponseDto>(contentString);
        var mapper = new TmdbMovieCollectionToMovie();

        var movies = dto?.Result
            .Select(movieCollection => mapper.Map(movieCollection))
            .ToList();

        return new MovieCollectionPage
        {
            Page = page,
            Movies = movies
        };
    }
}