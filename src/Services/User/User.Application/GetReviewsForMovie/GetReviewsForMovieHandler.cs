using MediatR;
using Microsoft.Extensions.Logging;
using User.Application.GetReviewsForMovie.Exceptions;
using User.Application.GetReviewsForMovie.Repository;
using User.Domain;
using User.Infrastructure;

namespace User.Application.GetReviewsForMovie;

public record GetReviewsForMovieQuery(int movieId) : IRequest<IReadOnlyCollection<Review>>;

public class GetReviewsForMovieHandler : IRequestHandler<GetReviewsForMovieQuery, IReadOnlyCollection<Review>>
{
    private readonly IGetReviewsForMovieRepository _repository;
    private readonly ILogger _logger;

    public GetReviewsForMovieHandler(IGetReviewsForMovieRepository repository,
        ILogger<GetReviewsForMovieHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<IReadOnlyCollection<Review>> Handle(GetReviewsForMovieQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            return await _repository.Get(request.movieId);
        }
        catch (Exception e)
        {
            _logger.LogError(LogEvent.Application,
                $"Failed to process {nameof(Handle)} in {nameof(GetReviewsForMovieHandler)}");
            throw new FailedToRetrieveReviewsException(request.movieId, null, e);
        }
    }
}