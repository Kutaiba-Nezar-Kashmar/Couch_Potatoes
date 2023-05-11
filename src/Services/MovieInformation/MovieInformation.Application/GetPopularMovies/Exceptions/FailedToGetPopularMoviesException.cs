namespace MovieInformation.Application.GetPopularMovies.Exceptions;

public class FailedToGetPopularMoviesException: Exception
{
    public FailedToGetPopularMoviesException():base()
    {
        
    }
    
    public FailedToGetPopularMoviesException(string msg): base(msg)
    {
    }
}