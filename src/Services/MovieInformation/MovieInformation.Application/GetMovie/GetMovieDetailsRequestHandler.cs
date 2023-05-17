using MediatR;
using Microsoft.Extensions.Logging;
using MovieInformation.Application.GetMovie.Exceptions;
using MovieInformation.Application.GetMovie.Repositories;
using MovieInformation.Domain.Models;

namespace MovieInformation.Application.GetMovie;

public record GetMovieDetailsRequest
(
    int movieId
) : IRequest<Movie>;

public class GetMovieDetailsRequestHandler : IRequestHandler<GetMovieDetailsRequest, Movie>
{
    private readonly IGetMovieRepository _getMovieRepository;
    private readonly ILogger _logger;

    public GetMovieDetailsRequestHandler(IGetMovieRepository getMovieRepository,
        ILogger<GetMovieDetailsRequestHandler> logger)
    {
        _getMovieRepository = getMovieRepository;
        _logger = logger;
    }

    public async Task<Movie> Handle(GetMovieDetailsRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            Movie getMovieRequest = await _getMovieRepository.GetMovie(request.movieId);
            IReadOnlyCollection<Keyword> getMovieKeywords = await _getMovieRepository.GetMovieKeywords(request.movieId);
            getMovieRequest.Keywords = getMovieKeywords;
            return getMovieRequest;
        }
        catch (Exception e)
        {
            _logger.LogError($"{nameof(GetMovieDetailsRequestHandler)}: ${e.Message}");
            throw new FailedToGetMovieDetailsException(
                $"Failed to retrieve movie details with movieId:${request.movieId}");
        }
    }
}