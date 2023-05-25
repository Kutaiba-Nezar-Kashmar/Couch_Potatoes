using MediatR;
using Microsoft.Extensions.Logging;
using MovieInformation.Application.GetRecommendedMovies.Exceptions;
using MovieInformation.Application.GetRecommendedMovies.Repositories;
using MovieInformation.Domain.Models;
using MovieInformation.Domain.Models.Movie;

namespace MovieInformation.Application.GetRecommendedMovies;

public record GetRecommendedMoviesRequest
(
    int Skip,
    int NumberOfPages,
    int MovieId
) : IRequest<MovieCollection>;

public class GetRecommendedMoviesRequestHandler : IRequestHandler<
    GetRecommendedMoviesRequest, MovieCollection>
{
    private readonly IGetRecommendedMoviesRepository
        _getRecommendedMoviesRepository;

    private readonly ILogger<GetRecommendedMoviesRequestHandler> _logger;

    public GetRecommendedMoviesRequestHandler
    (
        IGetRecommendedMoviesRepository getRecommendedMoviesRepository,
        ILogger<GetRecommendedMoviesRequestHandler> logger
    )
    {
        _getRecommendedMoviesRepository = getRecommendedMoviesRepository;
        _logger = logger;
    }

    public async Task<MovieCollection> Handle(
        GetRecommendedMoviesRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            List<Task<MovieCollectionPage>> getMoviesRequests = new();
            for (var i = 0; i < request.NumberOfPages; i++)
            {
                getMoviesRequests.Add(
                    _getRecommendedMoviesRepository.GetRecommendedMovies(
                        ResolvePage(request.Skip, i), request.MovieId));
            }

            return new MovieCollection
            {
                pages = await Task.WhenAll(getMoviesRequests)
            };
        }
        catch (Exception e)
        {
            _logger.LogError("{RecommendedMoviesRequestName}: ${EMessage}",
                nameof(GetRecommendedMoviesRequest), e.Message);
            throw new FailedToGetRecommendedMoviesException(
                $"Failed to retrieve recommended movies with movieId: ${request.MovieId}",
                e);
        }
    }

    private int ResolvePage(int skip, int page) => skip + page;
}