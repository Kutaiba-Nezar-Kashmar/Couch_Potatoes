using Microsoft.Extensions.Logging;
using MovieInformation.Application.GetRecommendedMovies.Exceptions;
using MovieInformation.Domain.Models;
using MovieInformation.Domain.Models.Movie;
using MovieInformation.Infrastructure.Exceptions;
using MovieInformation.Infrastructure.TmbdDto.ResponseDto;
using MovieInformation.Infrastructure.Util;

namespace MovieInformation.Application.GetRecommendedMovies.Repositories;

public class GetRecommendedMoviesRepository : IGetRecommendedMoviesRepository
{
    private readonly string _apiKey =
        Environment.GetEnvironmentVariable("TMDB_API_KEY");

    private readonly HttpClient _httpClient;
    private ILogger<GetRecommendedMoviesRepository> _logger;

    public GetRecommendedMoviesRepository
    (
        IHttpClientFactory httpClientFactory,
        ILogger<GetRecommendedMoviesRepository> logger
    )
    {
        _logger = logger;
        _httpClient = httpClientFactory.CreateClient("HTTP_CLIENT");
    }

    public async Task<MovieCollectionPage> GetRecommendedMovies(int page,
        int movieId)
    {
        try
        {
            _logger.LogInformation(
                "Get recommended movies with movie id: {Id}", movieId);
            var res =
                await _httpClient.GetAsync(
                    $"{movieId}/recommendations?api_key={_apiKey}");

            ValidateHttpResponse(res);
            var contentString = await res.Content.ReadAsStringAsync();
            ValidateResponseContent(contentString);

            var dto =
                JsonDeserializer.Deserialize<GetMovieCollectionResponseDto>(
                    contentString);
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
        catch (Exception e)
        {
            _logger.LogCritical(
                "Unable to get recommended movies with movie id: {Id}",
                movieId);
            throw new GetRecommendedMoviesException(
                $"Cannot get recommended movies: {e.Message}", e);
        }
    }


    private static void ValidateHttpResponse(
        HttpResponseMessage responseMessage)
    {
        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new HttpException
            (
                $"Unsuccessful response, code: {responseMessage}"
            );
        }
    }

    private static void ValidateResponseContent(string content)
    {
        if (string.IsNullOrEmpty(content) || string.IsNullOrWhiteSpace(content))
        {
            throw new HttpResponseException("Response is either null or empty");
        }
    }
}