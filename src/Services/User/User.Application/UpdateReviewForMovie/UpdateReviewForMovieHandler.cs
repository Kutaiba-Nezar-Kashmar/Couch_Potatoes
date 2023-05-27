using Google.Apis.Util;
using MediatR;
using Microsoft.Extensions.Logging;
using User.Application.UpdateReviewForMovie.Exceptions;
using User.Application.UpdateReviewForMovie.Repository;
using User.Domain;
using User.Domain.Exceptions;
using User.Infrastructure;

namespace User.Application.UpdateReviewForMovie;

public record UpdateReviewForMovieCommand
    (string userId, int movieId, Guid reviewId, int rating, string reviewText) : IRequest<Review>;

public class UpdateReviewForMovieHandler : IRequestHandler<UpdateReviewForMovieCommand, Review>
{
    private readonly ILogger _logger;
    private readonly IUpdateReviewForMovieRepository _repository;
    private readonly IAuthenticationRepository _auth;

    public UpdateReviewForMovieHandler(ILogger<UpdateReviewForMovieHandler> logger,
        IUpdateReviewForMovieRepository repository, IAuthenticationRepository auth)
    {
        _logger = logger;
        _repository = repository;
        _auth = auth;
    }

    public async Task<Review> Handle(UpdateReviewForMovieCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _auth.GetUserById(request.userId);
            if (user is null)
            {
                throw new UserDoesNotExistException(request.userId);
            }

            var review = await _repository.GetReviewById(request.reviewId);
            if (review is null)
            {
                throw new ReviewDoesNotExistException(request.reviewId, request.movieId);
            }

            var userHasReviewForMovie = review.MovieId == request.movieId;
            if (!userHasReviewForMovie)
            {
                throw new ReviewDoesNotExistException(request.reviewId, request.movieId);
            }

            var updatedReview =
                await _repository.UpdateReview(request.movieId, request.reviewId, request.rating, request.reviewText,
                    DateTime.UtcNow);

            if (updatedReview is null)
            {
                throw new FailedToUpdateReviewForMovieException(request.userId, request.movieId, request.reviewId,
                    "Failed to store updated review");
            }

            return updatedReview;
        }
        catch (Exception e) when (e is not FailedToUpdateReviewForMovieException)
        {
            _logger.LogError(LogEvent.Application, e,
                $"Failed to process {nameof(Handle)} for {nameof(UpdateReviewForMovieHandler)}");
            throw new FailedToUpdateReviewForMovieException(request.userId, request.movieId, request.reviewId, null, e);
        }
    }
}