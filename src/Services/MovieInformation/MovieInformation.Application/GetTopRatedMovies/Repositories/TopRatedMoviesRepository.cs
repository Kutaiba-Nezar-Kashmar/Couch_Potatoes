using Microsoft.Extensions.Logging;
using MovieInformation.Domain.Models;

namespace MovieInformation.Application.GetTopRatedMovies.Repositories;

public class TopRatedMoviesRepository: ITopRatedMoviesRepository
{
    private ILogger _logger;
    private HttpClient _client;
    
    public TopRatedMoviesRepository(IHttpClientFactory clientFactory, ILogger<TopRatedMoviesRepository> logger)
    {
        _logger = logger;
        _client = clientFactory.CreateClient("HTTP_CLIENT");
    }
    
    public Task<MovieCollection> GetTopRatedMovies()
    {
        throw new NotImplementedException();
    }
}