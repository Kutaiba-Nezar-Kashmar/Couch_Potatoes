namespace MovieInformation.Application.GetMovies.Exceptions;

public class GetMoviesKeywordsException : Exception
{
    public GetMoviesKeywordsException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}