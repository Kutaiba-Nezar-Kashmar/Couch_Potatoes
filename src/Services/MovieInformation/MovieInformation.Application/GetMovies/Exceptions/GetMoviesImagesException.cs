namespace MovieInformation.Application.GetMovies.Exceptions;

public class GetMoviesImagesException : Exception
{
    public GetMoviesImagesException(string message, Exception innerException) :
        base(message, innerException)
    {
    }
}