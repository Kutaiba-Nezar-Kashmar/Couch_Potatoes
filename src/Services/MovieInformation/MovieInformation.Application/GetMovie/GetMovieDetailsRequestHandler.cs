using MediatR;
using Microsoft.Extensions.Logging;
using MovieInformation.Application.GetMovie.Exceptions;
using MovieInformation.Application.GetMovie.Repositories;
using MovieInformation.Domain.Models;
using MovieInformation.Domain.Models.Movie;

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
            var getMovieRequest =
                await _getMovieRepository.GetMovie(request.MovieId);
            var getMovieKeywords =
                await _getMovieRepository.GetMovieKeywords(request.MovieId);
            var getMovieImages =
                await _getMovieRepository.GetMovieImages(request.MovieId);
            var getMovieVideos =
                await _getMovieRepository.GetMovieVideos(request.MovieId);
            getMovieRequest.Backdrops = getMovieImages.Backdrops;
            getMovieRequest.Logos = getMovieImages.Logos;
            getMovieRequest.Posters = getMovieImages.Posters;
            getMovieRequest.Keywords = getMovieKeywords;
            getMovieRequest.Videos = getMovieVideos.Results;
            return getMovieRequest;
        }
        catch (Exception e)
        {
            _logger.LogError("{MovieDetailsRequestHandlerName}: ${EMessage}",
                nameof(GetMovieDetailsRequestHandler), e.Message);
            throw new FailedToGetMovieDetailsException(
                $"Failed to retrieve movie details with movieId:${request.MovieId}");
        }
    }
}