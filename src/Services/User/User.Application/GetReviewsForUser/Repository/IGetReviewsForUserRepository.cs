using User.Domain;

namespace User.Application.GetReviewsForUser.Repository;

public interface IGetReviewsForUserRepository
{
    Task<IReadOnlyCollection<Review>> GetReviewsForUser(string userId);
}