using MediatR;
using Microsoft.Extensions.Logging;
using MovieInformation.Application.GetMovieCollection.Exceptions;
using MovieInformation.Application.GetMovieCollection.Repositories;
using MovieInformation.Domain.Models;
using MovieInformation.Domain.Models.Movie;

namespace MovieInformation.Application.GetMovieCollection;

public record GetMovieCollectionRequest
(
    int Skip,
    int NumberOfPages,
    string Type
) : IRequest<MovieCollection>;

public class GetMovieCollectionRequestHandler : IRequestHandler<
    GetMovieCollectionRequest, MovieCollection>
{
    private readonly IMovieCollectionRepository _movieCollectionRepository;
    private readonly ILogger<GetMovieCollectionRequestHandler> _logger;

    public GetMovieCollectionRequestHandler
    (
        IMovieCollectionRepository movieCollectionRepository,
        ILogger<GetMovieCollectionRequestHandler> logger
    )
    {
        _movieCollectionRepository = movieCollectionRepository;
        _logger = logger;
    }

    public async Task<MovieCollection> Handle(GetMovieCollectionRequest request,
        CancellationToken cancellationToken)
    {
        List<Task<MovieCollectionPage>> getMoviesRequests = new();
        for (var i = 0; i < request.NumberOfPages; i++)
        {
            try
            {
                _logger.LogInformation(
                    "Handling get movie collection with request: {Request}",
                    request);
                getMoviesRequests.Add(
                    _movieCollectionRepository.GetMovieCollection(
                        ResolvePage(request.Skip, i), request.Type));
            }
            catch (Exception e)
            {
                _logger.LogError(
                    "{MovieCollectionRequestHandlerName}: ${EMessage}",
                    nameof(GetMovieCollectionRequestHandler), e.Message);
                throw new FailedToGetMovieCollectionException(
                    $"Failed to retrieve movie collection of type:${request.Type}");
            }
        }

        return new MovieCollection
        {
            pages = await Task.WhenAll(getMoviesRequests)
        };
    }

    private static int ResolvePage(int skip, int page) => skip + page;
}