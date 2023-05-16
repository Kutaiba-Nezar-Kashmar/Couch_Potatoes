namespace User.Domain.Exceptions;

public class FailedToCreateReviewException: Exception
{
    public FailedToCreateReviewException(int movieId):base($"Failed to create review for movie {movieId}")
    {
        
    }
}