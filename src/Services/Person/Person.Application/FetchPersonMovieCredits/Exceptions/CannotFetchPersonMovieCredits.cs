namespace Person.Application.FetchPersonMovieCredits.Exceptions;

public class CannotFetchPersonMovieCredits : Exception
{
    public CannotFetchPersonMovieCredits(string message,
        Exception innerException) : base(message, innerException)
    {
    }
}