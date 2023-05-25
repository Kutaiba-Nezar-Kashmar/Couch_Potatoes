namespace MovieInformation.Application.GetMovie.Exceptions;

public class GetMovieKeywordsException : Exception
{
    public GetMovieKeywordsException(string message, Exception innerException) :
        base(message, innerException)
    {
    }
}