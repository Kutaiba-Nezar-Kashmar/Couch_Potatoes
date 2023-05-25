namespace MovieInformation.Application.GetMovie.Exceptions;

public class GetMovieException : Exception
{
    public GetMovieException(string message, Exception innerException) : base(
        message, innerException)
    {
    }
}