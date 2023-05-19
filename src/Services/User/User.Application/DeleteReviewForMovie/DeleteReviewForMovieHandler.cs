using MediatR;
using Microsoft.Extensions.Logging;
using User.Application.DeleteReview.Exceptions;
using User.Application.DeleteReview.Repository;
using User.Domain.Exceptions;
using User.Infrastructure;

namespace User.Application.DeleteReview;

public record DeleteReviewForMovieCommand(string userId, int movieId, Guid reviewId) : IRequest;

public class DeleteReviewForMovieHandler : IRequestHandler<DeleteReviewForMovieCommand>
{
    private readonly ILogger _logger;
    private readonly IDeleteReviewForMovieRepository _repository;
    private readonly IAuthenticationRepository _auth;

    public DeleteReviewForMovieHandler(ILogger<DeleteReviewForMovieHandler> logger,
        IDeleteReviewForMovieRepository repository, IAuthenticationRepository auth)
    {
        _logger = logger;
        _repository = repository;
        _auth = auth;
    }

    public async Task Handle(DeleteReviewForMovieCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _auth.GetUserById(request.userId);
            if (user is null)
            {
                throw new UserDoesNotExistException(request.userId);
            }

            var reviews = await _repository.GetReviewsForMovie(request.movieId);

            var existingReview = reviews.FirstOrDefault(review => review.ReviewId == request.reviewId);
            if (existingReview is null)
            {
                throw new ReviewDoesNotExistException(request.reviewId, request.movieId);
            }

            await _repository.DeleteReview(request.movieId, existingReview.ReviewId);
        }
        catch (Exception e) when (e is not UserDoesNotExistException or ReviewDoesNotExistException)
        {
            _logger.LogError(LogEvent.Application, e,
                $"Failed to proces {nameof(Handle)} in {nameof(DeleteReviewForMovieHandler)}: {e}");
            throw new FailedToDeleteReviewException(request.movieId, request.reviewId, null, e);
        }
    }
}