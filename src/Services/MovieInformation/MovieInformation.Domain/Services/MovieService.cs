using System.Text.Json.Nodes;
using MovieInformation.Domain.Repositories;

namespace MovieInformation.Domain.Services;

public class MovieService: IMovieService
{
    private readonly IMovieRepository _movieRepository;

    public MovieService(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }
    
    
    public async Task<JsonObject> GetPopularMovies()
    {
        return await _movieRepository.GetPopularMovies();
    }
}