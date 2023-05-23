using AutoMapper;
using Microsoft.Extensions.Logging;
using Search.Application.MultiSearch.Exceptions;
using Search.Domain.Models;
using Search.Infrastructure.Exceptions;
using Search.Infrastructure.Responses;
using Search.Infrastructure.Util;
using Search.Infrastructure.Util.Mappers;

namespace Search.Application.MultiSearch.Repositories;

public class MultiSearchRepository : IMultiSearchRepository
{
    private readonly ILogger<MultiSearchRepository> _logger;
    private readonly HttpClient _httpClient;
    private readonly IMapper _mapper;

    private readonly string _apiApi =
        Environment.GetEnvironmentVariable("TMDB_API_KEY");

    public MultiSearchRepository
    (
        ILogger<MultiSearchRepository> logger,
        IHttpClientFactory httpClientFactory,
        IMapper mapper
    )
    {
        _logger = logger;
        _httpClient = httpClientFactory.CreateClient("HTTP_CLIENT");;
        _mapper = mapper;
    }

    public async Task<MultiSearchResponse> MultiSearch(string query)
    {
        try
        {
            _logger.LogInformation(
                "Performing multi search with query parameter: {Query}", query);
            var response =
                await _httpClient.GetAsync(
                    $"search/multi?query={query}&api_key={_apiApi}");
            
            ValidateHttpResponse(response);
            var contentString = await response.Content.ReadAsStringAsync();
            ValidateResponseContent(contentString);
            
            var deserializedResponse =
                JsonDeserializer.Deserialize<TmdbMultiSearchResponseDto>(
                    contentString);
            
            var mapper = new TmdbMultiSearchToDomainMapper(_mapper);
            return mapper.Map(deserializedResponse!);
        }
        catch (Exception e)
        {
            _logger.LogCritical(
                "Unable to perform multi search with query parameter: {Query}",
                query);
            throw new MultiSearchException(
                $"Cannot do multi search with error: {e.Message}", e);
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