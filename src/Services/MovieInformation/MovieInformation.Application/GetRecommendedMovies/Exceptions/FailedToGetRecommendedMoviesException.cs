namespace MovieInformation.Application.GetRecommendedMovies.Exceptions;

public class FailedToGetRecommendedMoviesException : Exception
{
    public FailedToGetRecommendedMoviesException(string message,
        Exception innerException) : base(message, innerException)
    {
    }
}