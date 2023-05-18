namespace User.Application.UpdateReviewForMovie.Exceptions;

public class FailedToUpdateReviewForMovieException : Exception
{
    public FailedToUpdateReviewForMovieException(string userId, int movieId, Guid reviewId, string? reason = null,
        Exception? inner = null) : base(
        $"User {userId} Failed to update review {reviewId} for movie {movieId}: {reason ?? ""}", inner)
    {
    }
}