using Microsoft.Extensions.Logging;
using MovieInformation.Domain.Models;
using MovieInformation.Infrastructure.Exceptions;
using MovieInformation.Infrastructure.TmbdDto.KeywordsDto;
using MovieInformation.Infrastructure.TmbdDto.MovieDto;
using MovieInformation.Infrastructure.Util;

namespace MovieInformation.Application.GetMovies.Repository;

public class GetMoviesRepository: IGetMoviesRepository
{
    private string _apiKey = Environment.GetEnvironmentVariable("TMDB_API_KEY");
    
    private readonly HttpClient _httpClient;
    private readonly ILogger _logger;

    public GetMoviesRepository(IHttpClientFactory httpClientFactory, ILogger<GetMoviesRepository> logger)
    {
        _httpClient = httpClientFactory.CreateClient("HTTP_CLIENT");
        _logger = logger;
    }

    public async Task<IReadOnlyCollection<Movie>> GetMovies(IReadOnlyCollection<int> movieIds)
    {
        var getMovieTasks = movieIds.Select(GetMovie);
        var movies = await Task.WhenAll(getMovieTasks);
        return movies;
    }
    
    public async Task<Movie> GetMovie(int movieId)
    {
        var res = await _httpClient.GetAsync($"{movieId}?api_key={_apiKey}");

        if (!res.IsSuccessStatusCode)
        {
            _logger.LogError($"{nameof(GetMovie)}: Failed to fetch movie with ID: {movieId}, with status code: {res.StatusCode}");
            throw new HttpException(
                $"{nameof(GetMovie)}: Failed to fetch movie with ID: {movieId}, with status code: {res.StatusCode}");
        }

        var contentString = await res.Content.ReadAsStringAsync();
        var dto = JsonDeserializer.Deserialize<MovieDetail>(contentString);
        var mapper = new TmdbMovieToMovie();

        //map and return 
        if (dto == null)
        {
            _logger.LogError($"Failed to map movie {movieId} in {nameof(GetMoviesRepository)}");
            throw new MappingException($"Failed to map movie {movieId} in {nameof(GetMoviesRepository)} ");
        }
        
        var mappedMovie = mapper.Map(dto);
        return mappedMovie;
    }

    public async Task<IReadOnlyCollection<Keyword>> GetMovieKeywords(int movieId)
    {
        var res = await _httpClient.GetAsync($"{movieId}/keywords?api_key={_apiKey}");

        if (!res.IsSuccessStatusCode)
        {
            _logger.LogError($"{nameof(GetMovieKeywords)}: Failed to fetch keywords for movie: {movieId}, with status code: {res.StatusCode}");
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
            _logger.LogError($"{nameof(GetMovieKeywords)}: Failed to fetch images for movie: {movieId}, with status code: {res.StatusCode}");
            throw new HttpException(
                $"{nameof(GetMovieKeywords)}: Failed to fetch images for movie: {movieId}, with status code: {res.StatusCode}");
        }

        var contentString = await res.Content.ReadAsStringAsync();
        var dto = JsonDeserializer.Deserialize<MovieImagesResponseDto>(contentString);
        var mapper = new TmdbImagesToImages();
        return new List<Image>();
    }
}