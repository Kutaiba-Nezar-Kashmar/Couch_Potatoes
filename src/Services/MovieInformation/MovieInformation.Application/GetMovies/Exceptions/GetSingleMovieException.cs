namespace MovieInformation.Application.GetMovies.Exceptions;

public class GetSingleMovieException : Exception
{
    public GetSingleMovieException(string message, Exception innerException) :
        base(message, innerException)
    {
    }
}