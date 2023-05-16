using MediatR;
using Microsoft.Extensions.Logging;
using MovieInformation.Application.GetMovieCollection.Repositories;
using MovieInformation.Domain.Models;

namespace MovieInformation.Application.GetMovieCollection;

public record GetMovieCollectionRequest
(
    int Skip,
    int NumberOfPages,
    string Type
) : IRequest<MovieCollection>;

public class GetMovieCollectionRequestHandler : IRequestHandler<GetMovieCollectionRequest, MovieCollection>
{
    private readonly IMovieCollectionRepository _movieCollectionRepository;
    private readonly ILogger _logger;

    public GetMovieCollectionRequestHandler(IMovieCollectionRepository movieCollectionRepository, ILogger<GetMovieCollectionRequestHandler>logger)
    {
        _movieCollectionRepository = movieCollectionRepository;
        _logger = logger;
    }

    public async Task<MovieCollection> Handle(GetMovieCollectionRequest request,
        CancellationToken cancellationToken)
    {
        List<Task<MovieCollectionPage>> getMoviesRequests = new();
        for (int i = 0; i < request.NumberOfPages; i++)
        {
            try
            {
                getMoviesRequests.Add(_movieCollectionRepository.GetMovieCollection(ResolvePage(request.Skip, i), request.Type));
            }
            catch (Exception e)
            {
                _logger.LogError($"{nameof(GetMovieCollectionRequestHandler)}: ${e.Message}");
            }
        }

        return new MovieCollection
        {
            pages = await Task.WhenAll(getMoviesRequests)
        };
    }
    private int ResolvePage(int skip, int page) => skip + page;
}