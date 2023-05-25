using Microsoft.Extensions.Logging;
using MovieInformation.Application.GetMovieCollection.Exceptions;
using MovieInformation.Domain.Models;
using MovieInformation.Domain.Models.Movie;
using MovieInformation.Infrastructure.Exceptions;
using MovieInformation.Infrastructure.TmbdDto.ResponseDto;
using MovieInformation.Infrastructure.Util;

namespace MovieInformation.Application.GetMovieCollection.Repositories;

public class MovieCollectionRepository : IMovieCollectionRepository
{
    private readonly string _apiKey =
        Environment.GetEnvironmentVariable("TMDB_API_KEY");

    private readonly HttpClient _httpClient;
    private readonly ILogger<MovieCollectionRepository> _logger;

    public MovieCollectionRepository
    (
        IHttpClientFactory httpClientFactory,
        ILogger<MovieCollectionRepository> logger
    )
    {
        _logger = logger;
        _httpClient = httpClientFactory.CreateClient("HTTP_CLIENT");
    }

    public async Task<MovieCollectionPage> GetMovieCollection
    (int page,
        string collectionType)
    {
        try
        {
            _logger.LogInformation(
                "Get movie collection of type: {Type}", collectionType);
            var res =
                await _httpClient.GetAsync(
                    $"{collectionType}?api_key={_apiKey}");
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
                "Unable to get movie collection of type: {Type}",
                collectionType);
            throw new GetMovieCollectionException(
                $"Cannot get movie collection: {e.Message}", e);
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