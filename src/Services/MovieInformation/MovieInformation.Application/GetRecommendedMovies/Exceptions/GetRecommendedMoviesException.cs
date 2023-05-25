namespace MovieInformation.Application.GetRecommendedMovies.Exceptions;

public class GetRecommendedMoviesException : Exception
{
    public GetRecommendedMoviesException(string message,
        Exception innerException) : base(message, innerException)
    {
    }
}