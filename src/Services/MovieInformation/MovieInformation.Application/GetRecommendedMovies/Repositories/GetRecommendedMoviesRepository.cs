using MovieInformation.Domain.Models;
using MovieInformation.Infrastructure.Exceptions;
using MovieInformation.Infrastructure.TmbdDto.ResponseDto;
using MovieInformation.Infrastructure.Util;

namespace MovieInformation.Application.GetRecommendedMovies.Repositories;

public class GetRecommendedMoviesRepository : IGetRecommendedMoviesRepository
{
    private string _apiKey = Environment.GetEnvironmentVariable("TMDB_API_KEY");
    private HttpClient _httpClient;

    public GetRecommendedMoviesRepository(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("HTTP_CLIENT");
    }

    public async Task<MovieCollectionPage> GetRecommendedMovies(int page, int movieId)
    {
        var res = await _httpClient.GetAsync($"{movieId}/recommendations?api_key={_apiKey}");

        if (!res.IsSuccessStatusCode)
        {
            throw new HttpException(
                $"{nameof(GetRecommendedMovies)}: Failed to fetch recommended movies from movie with movieID: {movieId}, with status code: {res.StatusCode}");
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