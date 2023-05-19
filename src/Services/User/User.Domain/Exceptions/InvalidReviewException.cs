namespace User.Domain.Exceptions;

public class InvalidReviewException: Exception
{
    public InvalidReviewException(string? message = "Review is invalid"): base(message)
    {
    }

}