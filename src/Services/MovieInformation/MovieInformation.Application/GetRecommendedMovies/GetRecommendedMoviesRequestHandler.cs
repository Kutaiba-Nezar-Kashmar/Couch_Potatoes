using MediatR;
using Microsoft.Extensions.Logging;
using MovieInformation.Application.GetMovieCollection;
using MovieInformation.Application.GetRecommendedMovies.Repositories;
using MovieInformation.Domain.Models;

namespace MovieInformation.Application.GetRecommendedMovies;

public record GetRecommendedMoviesRequest
(
    int Skip,
    int NumberOfPages,
    int movieId
) : IRequest<MovieCollection>;

public class GetRecommendedMoviesRequestHandler : IRequestHandler<GetRecommendedMoviesRequest, MovieCollection>
{
    private readonly IGetRecommendedMoviesRepository _getRecommendedMoviesRepository;
    private readonly ILogger _logger;

    public GetRecommendedMoviesRequestHandler(IGetRecommendedMoviesRepository getRecommendedMoviesRepository, ILogger<GetRecommendedMoviesRequestHandler> logger)
    {
        _getRecommendedMoviesRepository = getRecommendedMoviesRepository;
        _logger = logger;
    }

    public async Task<MovieCollection> Handle(GetRecommendedMoviesRequest request, CancellationToken cancellationToken)
    {
         List<Task<MovieCollectionPage>> getMoviesRequests = new();
                for (int i = 0; i < request.NumberOfPages; i++)
                {
                    try
                    {
                        getMoviesRequests.Add(_getRecommendedMoviesRepository.GetRecommendedMovies(ResolvePage(request.Skip, i),request.movieId));
                    }
                    catch (Exception e)
                    {
                        _logger.LogError($"{nameof(GetRecommendedMoviesRequest)}: ${e.Message}");
                    }
                }
        
                return new MovieCollection
                {
                    pages = await Task.WhenAll(getMoviesRequests)
                };
    }
    private int ResolvePage(int skip, int page) => skip + page;
}