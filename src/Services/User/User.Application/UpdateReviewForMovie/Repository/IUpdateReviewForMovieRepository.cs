using User.Domain;

namespace User.Application.UpdateReviewForMovie.Repository;

public interface IUpdateReviewForMovieRepository
{
    Task<Review?> GetReviewById(Guid id);
    Task<Review?> UpdateReview(int movieId, Guid reviewId, int rating, string reviewText, DateTime updateTime);
}