using MovieInformation.Domain.Models;
using MovieInformation.Domain.Models.Movie;
using MovieInformation.Domain.Models.MovieImages;
using MovieInformation.Domain.Models.MovieVideos;

namespace MovieInformation.Application.GetMovie.Repositories;

public interface IGetMovieRepository
{
    Task<Movie> GetMovie(int movieId);
    Task<IReadOnlyCollection<Keyword>> GetMovieKeywords(int movieId);
    Task<MovieImagesResponse> GetMovieImages(int movieId);
    Task<MovieVideosResponse> GetMovieVideos(int movieId);
  
}