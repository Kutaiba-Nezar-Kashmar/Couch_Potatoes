using MovieInformation.Domain.Models;
using MovieInformation.Infrastructure.Exceptions;
using MovieInformation.Infrastructure.ResponseDtos;
using MovieInformation.Infrastructure.TmbdDto.KeywordsDto;
using MovieInformation.Infrastructure.TmbdDto.MovieDto;
using MovieInformation.Infrastructure.TmbdDto.ResponseDto;
using MovieInformation.Infrastructure.Util;

namespace MovieInformation.Application.GetMovie.Repositories;

public class GetMovieRepository : IGetMovieRepository
{
    // TODO: Make this configurable at runtime
    private string _apiKey = Environment.GetEnvironmentVariable("TMDB_API_KEY");
    
    private HttpClient _httpClient;

    public GetMovieRepository(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("HTTP_CLIENT");
    }

    public async Task<Movie> GetMovie(int movieId)
    {
        var res = await _httpClient.GetAsync($"{movieId}?api_key={_apiKey}");

        if (!res.IsSuccessStatusCode)
        {
            throw new HttpException(
                $"{nameof(GetMovie)}: Failed to fetch movie with ID: {movieId}, with status code: {res.StatusCode}");
        }

        var contentString = await res.Content.ReadAsStringAsync();
        var dto = JsonDeserializer.Deserialize<MovieDetail>(contentString);
        var mapper = new TmdbMovieToMovie();

        //  var movieDetail = dto?.Result;
        //map and return 
        var mappedMovie = mapper.Map(dto);
        return mappedMovie;
    }

    public async Task<IReadOnlyCollection<Keyword>> GetMovieKeywords(int movieId)
    {
        var res = await _httpClient.GetAsync($"{movieId}/keywords?api_key={_apiKey}");

        if (!res.IsSuccessStatusCode)
        {
            throw new HttpException(
                $"{nameof(GetMovieKeywords)}: Failed to fetch keywords for movie: {movieId}, with status code: {res.StatusCode}");
        }

        var contentString = await res.Content.ReadAsStringAsync();
        var dto = JsonDeserializer.Deserialize<KeywordsDetailsResponseDto>(contentString);
        var mapper = new TmdbKeywordsToKeywords();

        

       return dto!.keywords.Select(mapper.Map).ToList();
       
    }
    
    public async Task<IReadOnlyCollection<Image>> GetMovieImages(int movieId)
    {
        var res = await _httpClient.GetAsync($"{movieId}/images?api_key={_apiKey}");

        if (!res.IsSuccessStatusCode)
        {
            throw new HttpException(
                $"{nameof(GetMovieKeywords)}: Failed to fetch images for movie: {movieId}, with status code: {res.StatusCode}");
        }

        var contentString = await res.Content.ReadAsStringAsync();
        var dto = JsonDeserializer.Deserialize<MovieImagesResponseDto>(contentString);
        var mapper = new TmdbImagesToImages();



       // return dto.BackdropImages.Select(mapper.Map()).ToList();
        return null;
    }

  
}