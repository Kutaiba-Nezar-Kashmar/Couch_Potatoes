using User.Domain;

namespace User.Application.DeleteReview.Repository;

public interface IDeleteReviewForMovieRepository
{
    Task<IReadOnlyCollection<Review>> GetReviewsForMovie(int movieId);
    Task DeleteReview(int movieId, Guid reviewId);
}