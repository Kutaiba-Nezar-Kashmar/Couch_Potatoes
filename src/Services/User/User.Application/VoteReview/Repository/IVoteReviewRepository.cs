using User.Domain;

namespace User.Application.UpvoteReview.Repository;

public interface IVoteReviewRepository
{
    public Task VoteReview(int movieId, Review review, Vote vote);
}