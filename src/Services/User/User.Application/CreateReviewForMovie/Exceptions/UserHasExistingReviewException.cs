namespace User.Application.CreateReviewForMovie.Exceptions;

public class UserHasExistingReviewException : Exception
{
    public UserHasExistingReviewException(string userId, int movieId, Exception? inner = null) : base(
        $"User with id {userId} has already created a review for movie with id {movieId}", inner)
    {
    }
}