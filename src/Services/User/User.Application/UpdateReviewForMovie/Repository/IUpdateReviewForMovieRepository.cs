using User.Domain;

namespace User.Application.UpdateReviewForMovie.Exceptions;

public interface IUpdateReviewForMovieRepository
{
    Task<IReadOnlyCollection<Review>> GetReviewsForMovie(int movieId);
    Task<Review?> UpdateReview(int movieId, Guid reviewId, int rating, string reviewText, DateTime updateTime);
}