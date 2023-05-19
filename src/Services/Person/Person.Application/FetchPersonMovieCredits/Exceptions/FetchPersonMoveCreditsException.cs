namespace Person.Application.FetchPersonMovieCredits.Exceptions;

public class FetchPersonMoveCreditsException : Exception
{
    public FetchPersonMoveCreditsException(string message, Exception innerException) 
        : base(message, innerException)
    {
    }
}