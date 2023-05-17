using Metrics.Application.PersonMetrics.Exceptions;
using Metrics.Domain.Models;
using Metrics.Domain.Models.Person;
using Metrics.Infrastructure.Exceptions;
using Metrics.Infrastructure.Responses;
using Metrics.Infrastructure.Responses.PersonResponseDtos;
using Metrics.Infrastructure.Util;
using Metrics.Infrastructure.Util.Mappers;
using Microsoft.Extensions.Logging;

namespace Metrics.Application.PersonMetrics.Repositories;

public class FetchPersonMovieCreditsRepository : IFetchPersonMovieCreditsRepository
{
    private readonly ILogger<FetchPersonMovieCreditsRepository> _logger;

    private readonly string _apiApi =
        Environment.GetEnvironmentVariable("TMDB_API_KEY");

    private readonly HttpClient _httpClient;

    public FetchPersonMovieCreditsRepository
    (
        ILogger<FetchPersonMovieCreditsRepository> logger,
        IHttpClientFactory httpClientFactory
    )
    {
        _logger = logger;
        _httpClient = httpClientFactory.CreateClient("HTTP_CLIENT");
    }

    public async Task<PersonMovieCredits> FetchPersonMovieCreditsByPersonId
    (
        int personId
    )
    {
        try
        {
            _logger.LogInformation(
                "Fetching person movie credits with id: {PersonId}", personId);
            var response =
                await _httpClient.GetAsync(
                    $"person/{personId}/movie_credits?api_key={_apiApi}");

            ValidateHttpResponse(response);

            var contentString = await response.Content.ReadAsStringAsync();
            var deserializedResponse =
                JsonDeserializer.Deserialize<GetPersonMovieCreditsResponseDto>(
                    contentString);
            var mapper = new TmdpPersonMovieCreditToDomainMapper();

            var credits = mapper.Map(deserializedResponse!);
            return credits;
        }
        catch (Exception e)
        {
            _logger.LogCritical(
                "Unable to fetch person movie credits with id: {PersonId}",
                personId);
            throw new FetchPersonMoveCreditsException(
                $"Cannot fetch person movie credits: {e.Message}", e);
        }
    }

    public async Task<IReadOnlyCollection<Genre>> FetchGenres()
    {
        try
        {
            _logger.LogInformation("Fetching Genres");
            var response =
                await _httpClient.GetAsync($"genre/movie/list?api_key={_apiApi}");

            ValidateHttpResponse(response);

            var contentString = await response.Content.ReadAsStringAsync();
            var deserializedResponse =
                JsonDeserializer.Deserialize<GetGenreResponseDto>(contentString);
            
            var mapper = new TmdbGenreToDomainMapper();
            return deserializedResponse!.Genres.Select(mapper.Map).ToList();
        }
        catch (Exception e)
        {
            _logger.LogCritical("Unable to fetch Genre");
            throw new FetchMovieGenreException(
                $"Cannot fetch genres: {e.Message}", e);
        }
    }

    private static void ValidateHttpResponse(HttpResponseMessage responseMessage)
    {
        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new HttpException
            (
                $"Unsuccessful response, code: {responseMessage}"
            );
        }
    }
}