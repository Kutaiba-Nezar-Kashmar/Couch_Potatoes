namespace MovieInformation.Application.GetMovies.Exceptions;

public class FailedToGetMoviesException: Exception
{
    public FailedToGetMoviesException(IReadOnlyCollection<int> movieIds, string? reason = null, Exception? inner = null): base($"Failed to retrieve movies [{string.Join(", ", movieIds)}]: {reason}", inner)
    {
        
    }
}