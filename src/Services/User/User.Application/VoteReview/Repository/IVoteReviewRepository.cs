using User.Domain;

namespace User.Application.VoteReview.Repository;

public interface IVoteReviewRepository
{
    public Task VoteReview(int movieId, Review review, Vote vote);
    public Task<Review?> GetReviewById(Guid reviewId);
    public Task DeleteVote(int movieId, Guid reviewId, Guid voteId);
}