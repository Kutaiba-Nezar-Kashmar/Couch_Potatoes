using Microsoft.Extensions.Logging;
using MovieInformation.Application.GetMovie.Exceptions;
using MovieInformation.Application.GetMovies.Exceptions;
using MovieInformation.Domain.Models;
using MovieInformation.Domain.Models.Movie;
using MovieInformation.Domain.Models.MovieImages;
using MovieInformation.Domain.Models.MovieVideos;
using MovieInformation.Infrastructure.Exceptions;
using MovieInformation.Infrastructure.ResponseDtos;
using MovieInformation.Infrastructure.ResponseDtos.MediaResponses;
using MovieInformation.Infrastructure.TmbdDto.KeywordsDto;
using MovieInformation.Infrastructure.TmbdDto.MovieDto;
using MovieInformation.Infrastructure.Util;

namespace MovieInformation.Application.GetMovie.Repositories;

public class GetMovieRepository : IGetMovieRepository
{
    private readonly string _apiKey =
        Environment.GetEnvironmentVariable("TMDB_API_KEY");

    private readonly HttpClient _httpClient;
    private readonly ILogger<GetMovieRepository> _logger;

    public GetMovieRepository
    (
        IHttpClientFactory httpClientFactory,
        ILogger<GetMovieRepository> logger
    )
    {
        _logger = logger;
        _httpClient = httpClientFactory.CreateClient("HTTP_CLIENT");
    }

    public async Task<Movie> GetMovie(int movieId)
    {
        try
        {
            _logger.LogInformation(
                "Get movie with movie id: {Id}", movieId);
            var res =
                await _httpClient.GetAsync($"{movieId}?api_key={_apiKey}");

            ValidateHttpResponse(res);
            var contentString = await res.Content.ReadAsStringAsync();
            ValidateResponseContent(contentString);

            var dto = JsonDeserializer.Deserialize<MovieDetail>(contentString);
            var mapper = new TmdbMovieToMovie();
            var mappedMovie = mapper.Map(dto!);
            return mappedMovie;
        }
        catch (Exception e)
        {
            _logger.LogCritical(
                "Unable to get movie with movie id: {Id}", movieId);
            throw new GetMovieException($"Cannot get movie: {e.Message}", e);
        }
    }

    public async Task<IReadOnlyCollection<Keyword>> GetMovieKeywords(
        int movieId)
    {
        try
        {
            _logger.LogInformation(
                "Get movie keywords with movie id: {Id}", movieId);
            var res =
                await _httpClient.GetAsync(
                    $"{movieId}/keywords?api_key={_apiKey}");

            ValidateHttpResponse(res);
            var contentString = await res.Content.ReadAsStringAsync();
            ValidateResponseContent(contentString);

            var dto =
                JsonDeserializer.Deserialize<KeywordsDetailsResponseDto>(
                    contentString);
            var mapper = new TmdbKeywordsToKeywords();
            return dto!.keywords.Select(mapper.Map).ToList();
        }
        catch (Exception e)
        {
            _logger.LogCritical(
                "Unable to get movie keywords with movie id: {Id}",
                movieId);
            throw new GetMovieKeywordsException(
                $"Cannot get movie keywords: {e.Message}", e);
        }
    }

    public async Task<MovieImagesResponse> GetMovieImages(int movieId)
    {
        try
        {
            _logger.LogInformation(
                "Get movie images with movie id: {Id}", movieId);
            var res =
                await _httpClient.GetAsync(
                    $"{movieId}/images?api_key={_apiKey}");

            ValidateHttpResponse(res);
            var contentString = await res.Content.ReadAsStringAsync();
            ValidateResponseContent(contentString);

            var dto =
                JsonDeserializer.Deserialize<TmdbImagesResponseDto>(
                    contentString);
            var mapper = new TmdbImagesDtoToDomainMapper();
            var images = mapper.Map(dto!);
            return images;
        }
        catch (Exception e)
        {
            _logger.LogCritical(
                "Unable to get movie images with movie id: {Id}",
                movieId);
            throw new GetMoviesImagesException(
                $"Cannot get movie images: {e.Message}", e);
        }
    }

    public async Task<MovieVideosResponse> GetMovieVideos(int movieId)
    {
        try
        {
            _logger.LogInformation(
                "Get movie videos with movie id: {Id}", movieId);
            var res =
                await _httpClient.GetAsync(
                    $"{movieId}/videos?api_key={_apiKey}");

            ValidateHttpResponse(res);
            var contentString = await res.Content.ReadAsStringAsync();
            ValidateResponseContent(contentString);

            var dto =
                JsonDeserializer.Deserialize<TmdbVideosResponseDto>(
                    contentString);
            var mapper = new TmdbVideoToDomainMapper();
            var videos = mapper.Map(dto!);
            return videos;
        }
        catch (Exception e)
        {
            _logger.LogCritical(
                "Unable to get movie videos with movie id: {Id}",
                movieId);
            throw new GetMovieImagesException(
                $"Cannot get movie videos: {e.Message}", e);
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