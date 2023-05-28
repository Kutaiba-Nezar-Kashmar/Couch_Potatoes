using User.Domain;

namespace User.Application.DeleteReview.Repository;

public interface IDeleteReviewForMovieRepository
{
    Task<Review?> GetReviewById(Guid reviewId);
    Task DeleteReview(int movieId, Guid reviewId);
}