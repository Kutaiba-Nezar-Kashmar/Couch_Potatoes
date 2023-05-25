using MovieInformation.Domain.Models;
using MovieInformation.Domain.Models.MovieImages;

namespace MovieInformation.Application.GetMovie.Repositories;

public interface IGetMovieRepository
{
    Task<Movie> GetMovie(int movieId);
    Task<IReadOnlyCollection<Keyword>> GetMovieKeywords(int movieId);
    Task<MovieImagesResponse> GetMovieImages(int movieId);
  
}