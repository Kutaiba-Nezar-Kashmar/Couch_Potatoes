namespace MovieInformation.Application.GetMovie.Exceptions;

public class GetMovieImagesException : Exception
{
    public GetMovieImagesException(string message, Exception innerException) :
        base(message, innerException)
    {
    }
}