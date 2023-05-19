namespace MovieInformation.Application.GetMovie.Exceptions;

public class FailedToGetMovieDetailsException : Exception
{
    public FailedToGetMovieDetailsException() : base()
    {
    }

    public FailedToGetMovieDetailsException(string? message) : base(message)
    {
    }
}