namespace User.Application.DeleteReview.Exceptions;

public class FailedToDeleteReviewException : Exception
{
    public FailedToDeleteReviewException(int movieId, Guid reviewId, string? reason = null, Exception? inner = null) :
        base($"Failed to delete review {reviewId} for movie {movieId}: {reason ?? ""}", inner)
    {
    }
}