namespace Metrics.Application.PersonMetrics.Exceptions;

public class FetchMovieGenreException : Exception
{
    public FetchMovieGenreException(string message, Exception innerException) :
        base(message, innerException)
    {
    }
}