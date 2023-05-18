namespace User.Application.GetReviewsForMovie.Exceptions;

public class FailedToRetrieveReviewsException : Exception
{
    public FailedToRetrieveReviewsException(int movieId, string? reason = null, Exception? inner = null) : base(
        $"Failed to retrieve reviews for movie {movieId}: {reason ?? ""}", inner)

    {
    }
}