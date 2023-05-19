using User.Domain;

namespace User.Application.UpvoteReview.Exceptions;

public class FailedToVoteException : Exception
{
    public FailedToVoteException(string userId, int movieId, Guid reviewId, VoteDirection direction,
        string? reason = null, Exception? inner = null) : base(
        $"User {userId} failed to create vote for review {reviewId} in movie {movieId} with direction {direction.ToString()}: {reason ?? ""}",
        inner)
    {
    }
}