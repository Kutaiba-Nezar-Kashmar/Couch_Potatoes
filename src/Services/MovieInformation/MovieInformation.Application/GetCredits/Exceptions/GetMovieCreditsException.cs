namespace MovieInformation.Application.GetCredits.Exceptions;

public class GetMovieCreditsException : Exception
{
    public GetMovieCreditsException(string message, Exception innerException) :
        base(message, innerException)
    {
    }
}