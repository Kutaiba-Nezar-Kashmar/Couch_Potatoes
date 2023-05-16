using User.Domain;

namespace User.Application.CreateReviewForMovie.Repository;

public interface ICreateReviewForMovieRepository
{
    public Task CreateForMovie(int movieId, Review review);
}