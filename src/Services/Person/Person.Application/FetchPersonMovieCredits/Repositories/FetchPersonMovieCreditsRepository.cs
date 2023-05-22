using Microsoft.Extensions.Logging;
using Person.Application.FetchPersonMovieCredits.Exceptions;
using Person.Domain.Models.Person;
using Person.Infrastructure.Exceptions;
using Person.Infrastructure.Responses.PersonResponseDtos;
using Person.Infrastructure.Util;
using Person.Infrastructure.Util.Mappers;

namespace Person.Application.FetchPersonMovieCredits.Repositories;

public class
    FetchPersonMovieCreditsRepository : IFetchPersonMovieCreditsRepository
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
}