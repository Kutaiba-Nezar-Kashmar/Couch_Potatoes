using MediatR;
using Microsoft.Extensions.Logging;
using User.Application.GetReviewsForMovie;
using User.Application.UpvoteReview.Exceptions;
using User.Application.UpvoteReview.Repository;
using User.Domain;
using User.Domain.Exceptions;
using User.Infrastructure;

namespace User.Application.UpvoteReview;

public record VoteReviewCommand(string userId, int movieId, Guid reviewId, VoteDirection direction) : IRequest<Vote>;

public class VoteReviewHandler : IRequestHandler<VoteReviewCommand, Vote>
{
    private readonly ILogger _logger;
    private readonly IVoteReviewRepository _repository;
    private readonly IAuthenticationRepository _auth;

    public VoteReviewHandler(ILogger<VoteReviewHandler> logger, IVoteReviewRepository repository,
        IAuthenticationRepository auth)
    {
        _logger = logger;
        _repository = repository;
        _auth = auth;
    }

    /// <summary>
    /// Creates a new Vote with direction {up, down} for a review.
    /// When an upvote already exists, it simply updates the existing vote.
    /// If an vote in the same direction already exists, then delete that vote.
    /// </summary>
    public async Task<Vote> Handle(VoteReviewCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _auth.GetUserById(request.userId);

            if (user is null)
            {
                throw new UserDoesNotExistException(request.userId);
            }

            var reviews = await _repository.GetReviewsForMovie(request.movieId);

            var reviewDoesNotExist = reviews.All(r => r.ReviewId != request.reviewId);
            if (reviewDoesNotExist)
            {
                throw new ReviewDoesNotExistException(request.reviewId, request.movieId);
            }

            var review = reviews.First(r => r.ReviewId == request.reviewId);
            var existingVote = review.Votes.FirstOrDefault(v => v.UserId == request.userId);

            if (existingVote is null)
            {
                var newVote = new Vote
                {
                    Direction = request.direction,
                    UserId = request.userId,
                    Id = Guid.NewGuid()
                };
                await _repository.VoteReview(request.movieId, review, newVote);
                return (newVote);
            }

            if (existingVote.Direction == request.direction)
            {
                await _repository.DeleteVote(request.movieId, request.reviewId, existingVote.Id);
                return (null);
            }

            await _repository.DeleteVote(request.movieId, request.reviewId, existingVote.Id);
            existingVote.Direction = request.direction;
            await _repository.VoteReview(request.movieId, review, existingVote);
            return existingVote;
        }
        catch (Exception e) when (e is not UserDoesNotExistException or ReviewDoesNotExistException)
        {
            _logger.LogError(LogEvent.Application,
                $"Failed to process {nameof(Handle)} in {nameof(VoteReviewHandler)}");
            throw new FailedToVoteException(request.userId, request.movieId, request.reviewId, request.direction, null,
                e);
        }
    }
}