namespace User.Application.RemoveMovieFromFavorites.Exceptions;

public class FailedToRemoveMovieFromFavoritesException : Exception
{
    public FailedToRemoveMovieFromFavoritesException(string userId, int movieId, Exception? inner = null) : base(
        $"Failed to remove movie with id {movieId} from favorites for user with id {userId}", inner)
    {
    }
}