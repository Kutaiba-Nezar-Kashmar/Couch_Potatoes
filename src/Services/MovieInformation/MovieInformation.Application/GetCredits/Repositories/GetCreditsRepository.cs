using MovieInformation.Domain.Models;
using MovieInformation.Infrastructure.Exceptions;
using MovieInformation.Infrastructure.ResponseDtos;
using MovieInformation.Infrastructure.Util;

namespace MovieInformation.Application.GetCredits.Repositories;

public class GetCreditsRepository : IGetCreditsRepository
{
    
    private string _apiKey = Environment.GetEnvironmentVariable("TMDB_API_KEY");
    
    private HttpClient _httpClient;

    public GetCreditsRepository(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("HTTP_CLIENT");
    }
    
    public async Task<PersonMovieCredits> GetMovieCredits(int movieId)
    {
        var res = await _httpClient.GetAsync($"{movieId}/credits?api_key={_apiKey}");

        if (!res.IsSuccessStatusCode)
        {
            throw new HttpException(
                $"{nameof(GetMovieCredits)}: Failed to fetch credits for movie: {movieId}, with status code: {res.StatusCode}");
        }

        var contentString = await res.Content.ReadAsStringAsync();
        var dto = JsonDeserializer.Deserialize<GetPersonMovieCreditsResponseDto>(contentString);
        var mapper = new TmdbPersonMovieCreditToDomainMapper();

        var credits = mapper.Map(dto);
        return credits;
    }
}