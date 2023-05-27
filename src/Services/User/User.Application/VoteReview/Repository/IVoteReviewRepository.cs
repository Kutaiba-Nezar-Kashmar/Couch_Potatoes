using User.Domain;

namespace User.Application.UpvoteReview.Repository;

public interface IVoteReviewRepository
{
    public Task VoteReview(int movieId, Review review, Vote vote);
    public Task<Review?> GetReviewById(Guid reviewId);
    public Task<IReadOnlyCollection<Review>> GetReviewsForMovie(int movieId);
    public Task DeleteVote(int movieId, Guid reviewId, Guid voteId);
}