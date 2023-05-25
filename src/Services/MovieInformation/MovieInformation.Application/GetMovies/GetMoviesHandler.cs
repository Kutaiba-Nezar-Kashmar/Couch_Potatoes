using MediatR;
using Microsoft.Extensions.Logging;
using MovieInformation.Application.GetMovies.Exceptions;
using MovieInformation.Application.GetMovies.Repository;
using MovieInformation.Domain.Models;

namespace MovieInformation.Application.GetMovies;

public record class GetMoviesQuery(IReadOnlyCollection<int> movieIds) : IRequest<IReadOnlyCollection<Movie>>;

public class GetMoviesHandler : IRequestHandler<GetMoviesQuery, IReadOnlyCollection<Movie>>
{
    private readonly ILogger _logger;
    private readonly IGetMoviesRepository _repository;

    public GetMoviesHandler(ILogger<GetMoviesHandler> logger, IGetMoviesRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<IReadOnlyCollection<Movie>> Handle(GetMoviesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _repository.GetMovies(request.movieIds);
            foreach (var r in response)
            {
                r.Posters = (await _repository.GetMovieImages(r.Id)).Posters;
            }

            return response;
        }
        catch (Exception e)
        {
            _logger.LogError(1, e, $"Failed to process {nameof(Handle)} in {nameof(GetMoviesHandler)}: {e}");
            throw new FailedToGetMoviesException(request.movieIds, "", e);
        }
    }
}