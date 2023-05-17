using Microsoft.Extensions.Logging;
using Person.Application.FetchPersonDetails.Exceptions;
using Person.Domain.Models.Person;
using Person.Infrastructure.Exceptions;
using Person.Infrastructure.TmdbDtos.PersonDetailsDto;
using Person.Infrastructure.Util;
using Person.Infrastructure.Util.Mappers;

namespace Person.Application.FetchPersonDetails.Repositories;

public class FetchPersonDetailsRepository : IFetchPersonDetailsRepository
{
    private readonly ILogger<FetchPersonDetailsRepository> _logger;

    private readonly string _apiApi =
        Environment.GetEnvironmentVariable("TMDB_API_KEY");

    private readonly HttpClient _httpClient;

    public FetchPersonDetailsRepository
    (
        ILogger<FetchPersonDetailsRepository> logger,
        IHttpClientFactory httpClientFactory
    )
    {
        _logger = logger;
        _httpClient = httpClientFactory.CreateClient("HTTP_CLIENT");
    }

    public async Task<PersonDetails> FetchPersonDetailsById(int personId)
    {
        try
        {
            _logger.LogInformation(
                "Fetching person details with id: {PersonId}", personId);
            var response =
                await _httpClient.GetAsync(
                    $"person/{personId}?api_key={_apiApi}");
            
            ValidateHttpResponse(response);
            var contentString = await response.Content.ReadAsStringAsync();
            ValidateResponseContent(contentString);
            
            var deserializedResponse =
                JsonDeserializer.Deserialize<TmdbPersonDetailsDto>(
                    contentString);
            
            var mapper = new TmdbPersonDetailsDtoToDomainMapper();
            return mapper.Map(deserializedResponse!);
        }
        catch (Exception e)
        {
            _logger.LogCritical(
                "Unable to fetch person details with id: {PersonId}",
                personId);
            throw new FetchPersonDetailsException(
                $"Cannot fetch person details: {e.Message}", e);
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