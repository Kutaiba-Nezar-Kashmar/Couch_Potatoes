namespace MovieInformation.Application.GetMovieCollection.Exceptions;

public class GetMovieCollectionException : Exception
{
    public GetMovieCollectionException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}