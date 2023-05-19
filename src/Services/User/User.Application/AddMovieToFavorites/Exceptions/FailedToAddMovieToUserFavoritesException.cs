namespace User.Application.AddMovieToFavorites.Exceptions;

public class FailedToAddMovieToUserFavoritesException : Exception
{
    public FailedToAddMovieToUserFavoritesException(string userId, int movieId, Exception? inner = null) : base(
        $"Failed to add movie with id {movieId} to favorites for user with id {userId}", inner)
    {
    }
}