using MovieInformation.Domain.Models;
using MovieInformation.Domain.Models.MovieImages;

namespace MovieInformation.Application.GetMovies.Repository;

public interface IGetMoviesRepository
{
    Task<IReadOnlyCollection<Movie>> GetMovies(IReadOnlyCollection<int> movieIds);
    Task<MovieImagesResponse> GetMovieImages(int movieId);
    Task<IReadOnlyCollection<Keyword>> GetMovieKeywords(int movieId);
}