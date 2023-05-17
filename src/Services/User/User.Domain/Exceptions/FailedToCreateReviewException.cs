namespace User.Domain.Exceptions;

public class FailedToCreateReviewException: Exception
{
    public FailedToCreateReviewException(int movieId,string? reason,  Exception? inner = null):base($"Failed to create review for movie {movieId}: {reason?? ""}", inner)
    {
        
    }
}