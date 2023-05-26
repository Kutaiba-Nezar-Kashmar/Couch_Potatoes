using MediatR;
using Microsoft.Extensions.Logging;
using MovieInformation.Application.GetMovie.Exceptions;
using MovieInformation.Application.GetMovie.Repositories;
using MovieInformation.Domain.Models;
using MovieInformation.Domain.Models.Movie;
using MovieInformation.Domain.Models.MovieImages;
using MovieInformation.Domain.Models.MovieReleaseDates;
using MovieInformation.Domain.Models.MovieVideos;

namespace MovieInformation.Application.GetMovie;

public record GetMovieDetailsRequest
(
    int MovieId
) : IRequest<Movie>;

public class
    GetMovieDetailsRequestHandler : IRequestHandler<GetMovieDetailsRequest,
        Movie>
{
    private readonly IGetMovieRepository _getMovieRepository;
    private readonly ILogger<GetMovieDetailsRequestHandler> _logger;

    public GetMovieDetailsRequestHandler
    (
        IGetMovieRepository getMovieRepository,
        ILogger<GetMovieDetailsRequestHandler> logger
    )
    {
        _getMovieRepository = getMovieRepository;
        _logger = logger;
    }

    public async Task<Movie> Handle
    (
        GetMovieDetailsRequest request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            _logger.LogInformation(
                "Handling get movie with movie request {Request}", request);
            return await MovieRequest(request);
        }
        catch (Exception e)
        {
            _logger.LogError("{MovieDetailsRequestHandlerName}: ${EMessage}",
                nameof(GetMovieDetailsRequestHandler), e.Message);
            throw new FailedToGetMovieDetailsException(
                $"Failed to retrieve movie details with movieId:${request.MovieId}");
        }
    }

    private async Task<Movie> MovieRequest(GetMovieDetailsRequest request)
    {
        var movie =
            await _getMovieRepository.GetMovie(request.MovieId);

        movie.Backdrops = await MovieBackdrops(request.MovieId);
        movie.Logos = await MovieLogos(request.MovieId);
        movie.Posters = await MoviePosters(request.MovieId);
        movie.Keywords = await MovieKeywords(request.MovieId);
        movie.Videos = await MovieVideos(request.MovieId);
        movie.ReleaseDates = await ReleaseDates(request.MovieId);
        return movie;
    }

    private async Task<IReadOnlyCollection<Keyword>> MovieKeywords(int movieId)
    {
        return await _getMovieRepository.GetMovieKeywords(movieId);
    }

    private async Task<IReadOnlyCollection<MovieImage>> MoviePosters(
        int movieId)
    {
        var im = await _getMovieRepository.GetMovieImages(movieId);
        return im.Posters;
    }

    private async Task<IReadOnlyCollection<MovieImage>> MovieLogos(
        int movieId)
    {
        var im = await _getMovieRepository.GetMovieImages(movieId);
        return im.Logos;
    }

    private async Task<IReadOnlyCollection<MovieImage>> MovieBackdrops(
        int movieId)
    {
        var im = await _getMovieRepository.GetMovieImages(movieId);
        return im.Backdrops;
    }

    private async Task<IReadOnlyCollection<MovieVideo>> MovieVideos(int movieId)
    {
        var getMovieVideos =
            await _getMovieRepository.GetMovieVideos(movieId);
        return getMovieVideos.Results;
    }

    private async Task<IReadOnlyCollection<MovieReleaseDate>> ReleaseDates(
        int movieId)
    {
        var releaseDates =
            await _getMovieRepository.GetMovieReleaseDates(movieId);
        return releaseDates.Results;
    }
}