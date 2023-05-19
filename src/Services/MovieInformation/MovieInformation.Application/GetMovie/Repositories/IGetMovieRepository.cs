using MovieInformation.Domain.Models;

namespace MovieInformation.Application.GetMovie.Repositories;

public interface IGetMovieRepository
{
    Task<Movie> GetMovie(int movieId);
    Task<IReadOnlyCollection<Keyword>> GetMovieKeywords(int movieId);
    Task<IReadOnlyCollection<Image>> GetMovieImages(int movieId);
}