using User.Domain;

namespace User.Application.GetReviewsForMovie.Repository;

public interface IGetReviewsForMovieRepository
{
    public Task<IReadOnlyCollection<Review>> Get(int movieId);
}