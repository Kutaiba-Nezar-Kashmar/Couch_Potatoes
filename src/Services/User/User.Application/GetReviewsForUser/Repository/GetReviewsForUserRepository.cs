using System.Text.Json;
using Google.Cloud.Firestore;
using Microsoft.Extensions.Logging;
using User.Application.CreateReviewForMovie.Repository;
using User.Domain;
using User.Infrastructure;

namespace User.Application.GetReviewsForUser.Repository;

public class GetReviewsForUserRepository : IGetReviewsForUserRepository
{
    private readonly CollectionReference _collectionReference;
    private readonly ILogger _logger;
    private const string CollectionName = "Reviews";

    public GetReviewsForUserRepository(ILogger<GetReviewsForUserRepository> logger)
    {
        _logger = logger;
        _collectionReference = Firestore.Get().Collection(CollectionName);
    }

    public async Task<IReadOnlyCollection<Review>> GetReviewsForUser(string userId)
    {
        try
        {
            var reviews = await GetAllReviews();
            return reviews.Where(review => review.UserId == userId).ToList();
        }
        catch (Exception e)
        {
            _logger.LogError(LogEvent.Infrastructure, e, $"Failed to get reviews from Firestore: {e}");
            throw;
        }
    }

    private async Task<IReadOnlyCollection<Review>> GetAllReviews()
    {
        try
        {
            var allMovieReviewsSnapshot = await _collectionReference.GetSnapshotAsync();
            return allMovieReviewsSnapshot.Documents
                .Where(movieSnapshot => movieSnapshot.Exists)
                .SelectMany(snapshot =>
                {
                    var movieDict = snapshot.ToDictionary();
                    if (movieDict is null)
                    {
                        return new List<Review>();
                    }

                    var reviews = movieDict["Reviews"];
                    if (reviews is null)
                    {
                        return new List<Review>();
                    }

                    return JsonSerializer.Deserialize<IEnumerable<FirestoreReviewDto>>(
                            JsonSerializer.Serialize(reviews,
                                new JsonSerializerOptions() {PropertyNameCaseInsensitive = true}))?
                        .Select(dto => dto.ToDomainReview()).ToList() ?? new List<Review>();
                })
                .ToList();
        }
        catch (Exception e)
        {
            _logger.LogError(LogEvent.Infrastructure, e, $"Failed to get reviews from Firestore: {e}");
            throw;
        }
    }
}