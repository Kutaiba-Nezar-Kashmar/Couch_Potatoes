namespace User.Domain.Exceptions;

public class ReviewDoesNotExistException : Exception
{
    public ReviewDoesNotExistException(Guid reviewId, int movieId, Exception? inner = null)
        : base($"Review with id {reviewId.ToString()} does not exist for movie with id {movieId}", inner)

    {
    }
}