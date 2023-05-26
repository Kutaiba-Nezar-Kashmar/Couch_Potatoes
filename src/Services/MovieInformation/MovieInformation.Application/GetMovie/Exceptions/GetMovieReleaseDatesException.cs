namespace MovieInformation.Application.GetMovie.Exceptions;

public class GetMovieReleaseDatesException : Exception
{
    public GetMovieReleaseDatesException(string message,
        Exception innerException) : base(message, innerException)
    {
    }
}