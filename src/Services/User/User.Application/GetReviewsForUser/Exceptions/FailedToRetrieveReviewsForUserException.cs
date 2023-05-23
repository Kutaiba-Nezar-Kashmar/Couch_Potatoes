namespace User.Application.GetReviewsForUser.Exceptions;

public class FailedToRetrieveReviewsForUserException: Exception
{
    public FailedToRetrieveReviewsForUserException(string userId, string? reason = null, Exception? inner = null): base($"Failed to retrieve reviews for user {userId}: {reason ?? ""}", inner)
    {
        
    }   
}