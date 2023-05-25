using Microsoft.Extensions.Logging;
using MovieInformation.Application.GetCredits.Exceptions;
using MovieInformation.Domain.Models;
using MovieInformation.Infrastructure.Exceptions;
using MovieInformation.Infrastructure.ResponseDtos;
using MovieInformation.Infrastructure.Util;

namespace MovieInformation.Application.GetCredits.Repositories;

public class GetCreditsRepository : IGetCreditsRepository
{
    private readonly string _apiKey =
        Environment.GetEnvironmentVariable("TMDB_API_KEY");
    private readonly HttpClient _httpClient;
    private readonly ILogger<GetCreditsRepository> _logger;

    public GetCreditsRepository
    (
        IHttpClientFactory httpClientFactory,
        ILogger<GetCreditsRepository> logger
    )
    {
        _logger = logger;
        _httpClient = httpClientFactory.CreateClient("HTTP_CLIENT");
    }

    public async Task<PersonMovieCredits> GetMovieCredits(int movieId)
    {
        try
        {
            _logger.LogInformation(
                "Get movie credits with movie id: {Id}", movieId);
            var res =
                await _httpClient.GetAsync(
                    $"{movieId}/credits?api_key={_apiKey}");

            ValidateHttpResponse(res);
            var contentString = await res.Content.ReadAsStringAsync();
            ValidateResponseContent(contentString);

            var dto =
                JsonDeserializer.Deserialize<GetPersonMovieCreditsResponseDto>(
                    contentString);
            var mapper = new TmdbPersonMovieCreditToDomainMapper();

            var credits = mapper.Map(dto!);
            return credits;
        }
        catch (Exception e)
        {
            _logger.LogCritical(
                "Unable to get movie credits with movie id: {Id}",
                movieId);
            throw new GetMovieCreditsException(
                $"Cannot get movie credits: {e.Message}", e);
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