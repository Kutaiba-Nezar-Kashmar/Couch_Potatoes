﻿using MovieInformation.Domain.Models;
using MovieInformation.Infrastructure.Exceptions;
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
        var dto = JsonDeserializer.Deserialize<GetMovieDetailsResponseDto>(contentString);
        var mapper = new TmdbMovieCollectionToMovie();

      //  var movie = dto?.Result
      //      .Select(movie => mapper.Map(movie))
        //    .ToList();

        return new Movie
        {
           
        };
    }
}