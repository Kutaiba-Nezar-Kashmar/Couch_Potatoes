using User.Domain;

namespace User.Test;

public static class TestUtil
{
    public static CouchPotatoUser CreateGenericUser(Action<CouchPotatoUser>? configureUser = null)
    {
        var id = Guid.NewGuid().ToString();
        var randomString = id.Split("-")[0];
        var email = randomString;
        var domain = "test.com";
        
        var user = new CouchPotatoUser
        {
            Email = $"{email}@{domain}",
            Id = id,
            AvatarUri = "",
            CreatedTimestamp = DateTime.Now,
            DisplayName = $"TestHestSlayer{randomString}",
            FavoriteMovies = new List<int>(),
            LastSignInTimestamp = DateTime.Now
        };

        configureUser?.Invoke(user);
        
        return user;
    }

    public static Review CreateReview(string userId, int movieId, Action<Review>? configureReview = null)
    {
        var review = new Review
        {
            Rating = 5,
            MovieId = movieId,
            CreationDate = DateTime.UtcNow,
            LastUpdatedDate = DateTime.UtcNow,
            ReviewText = "meh",
            UserId = userId,
            Votes = new List<Vote>(),
            ReviewId = Guid.NewGuid()
        };
        
        configureReview?.Invoke(review);

        return review;
    }
}