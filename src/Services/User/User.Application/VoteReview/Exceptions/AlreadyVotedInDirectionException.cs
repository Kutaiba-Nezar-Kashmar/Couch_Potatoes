using User.Domain;

namespace User.Application.UpvoteReview.Exceptions;

public class AlreadyVotedInDirectionException : Exception
{
    public AlreadyVotedInDirectionException(string userId, Guid reviewId, VoteDirection direction, Exception? inner)
        : base($"User with id {userId} has already {direction.ToString()}voted review with id {reviewId}", inner)
    {
    }
}