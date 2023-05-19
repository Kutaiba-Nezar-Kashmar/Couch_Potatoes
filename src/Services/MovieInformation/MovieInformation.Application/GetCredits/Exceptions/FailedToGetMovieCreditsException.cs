namespace MovieInformation.Application.GetCredits.Exceptions;

public class FailedToGetMovieCreditsException : Exception
{
    public FailedToGetMovieCreditsException() : base()
    {
    }

    public FailedToGetMovieCreditsException(string? message) : base(message)
    {
    }
}