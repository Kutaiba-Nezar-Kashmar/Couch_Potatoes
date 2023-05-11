using System.Runtime.CompilerServices;
using MediatR;
using Microsoft.Extensions.Logging;
using MovieInformation.Application.GetPopularMovies.Repositories;
using MovieInformation.Domain.Models;

namespace MovieInformation.Application.GetPopularMovies;

public record GetPopularMoviesRequest
(
    int Skip,
    int NumberOfPages
) : IRequest<MovieCollection>;

public class GetPopularMoviesRequestHandler : IRequestHandler<GetPopularMoviesRequest, MovieCollection>
{
    private readonly IPopularMovieRepository _popularMovieRepository;
    private readonly ILogger _logger;

    public GetPopularMoviesRequestHandler(IPopularMovieRepository popularMovieRepository, ILogger<GetPopularMoviesRequestHandler>logger)
    {
        _popularMovieRepository = popularMovieRepository;
        _logger = logger;
    }

    public async Task<MovieCollection> Handle(GetPopularMoviesRequest request,
        CancellationToken cancellationToken)
    {
        List<Task<MovieCollectionPage>> getMoviesRequests = new();
        for (int i = 0; i < request.NumberOfPages; i++)
        {
            try
            {
                getMoviesRequests.Add(_popularMovieRepository.GetPopularMovies(ResolvePage(request.Skip, i)));
            }
            catch (Exception e)
            {
                _logger.LogError($"{nameof(GetPopularMoviesRequestHandler)}: ${e.Message}");
            }
        }

        return new MovieCollection
        {
            pages = await Task.WhenAll(getMoviesRequests)
        };
    }
    private int ResolvePage(int skip, int page) => skip + page;
}