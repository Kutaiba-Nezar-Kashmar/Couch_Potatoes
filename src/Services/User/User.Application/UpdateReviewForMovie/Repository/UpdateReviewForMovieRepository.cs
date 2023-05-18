using System.Text.Json;
using Google.Apis.Logging;
using Google.Cloud.Firestore;
using Microsoft.Extensions.Logging;
using User.Application.CreateReviewForMovie.Repository;
using User.Domain;
using User.Infrastructure;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace User.Application.UpdateReviewForMovie.Exceptions;

public class UpdateReviewForMovieRepository : IUpdateReviewForMovieRepository
{
    private readonly ILogger _logger;
    private readonly CollectionReference _collectionReference;
    private const string CollectionName = "Reviews";

    public UpdateReviewForMovieRepository(ILogger<UpdateReviewForMovieRepository> logger)
    {
        _collectionReference = Firestore.Get().Collection(CollectionName);
        _logger = logger;
    }

    public async Task<IReadOnlyCollection<Review>> GetReviewsForMovie(int movieId)
    {
        var doc = _collectionReference.Document(movieId.ToString());
        var snapshot = await doc.GetSnapshotAsync();

        if (!snapshot.Exists)
        {
            return new List<Review>();
        }

        var elements = snapshot.ToDictionary();
        var reviews = elements["Reviews"];

        if (reviews is null)
        {
            return new List<Review>();
        }

        return JsonSerializer.Deserialize<IEnumerable<FirestoreReviewDto>>(
                JsonSerializer.Serialize(reviews, new JsonSerializerOptions() {PropertyNameCaseInsensitive = true}))
            .Select(dto => dto.ToDomainReview()).ToList();
    }

    public async Task<Review?> UpdateReview(int movieId, Guid reviewId, int rating, string reviewText,
        DateTime updateTime)
    {
        var doc = _collectionReference.Document(movieId.ToString());
        var snapshot = await doc.GetSnapshotAsync();
        if (!snapshot.Exists)
        {
            return null;
        }

        try
        {
            var reviews = await GetReviewsForMovie(movieId);
            var reviewToUpdate = reviews.First(review => review.ReviewId == reviewId);

            reviewToUpdate.ReviewText = reviewText;
            reviewToUpdate.Rating = rating;
            reviewToUpdate.LastUpdatedDate = updateTime;

            var updatedReviewsState = reviews
                .Where(review => review.ReviewId != reviewId) // Filter out outdated state
                .Concat(new List<Review>() {reviewToUpdate}); // Insert updated state

            doc.SetAsync(new Dictionary<string, object>()
            {
                {"Reviews", updatedReviewsState.Select(rev => rev.ToFirestoreReview())}
            });

            return reviewToUpdate;
        }
        catch (Exception e)
        {
            _logger.LogError(LogEvent.Infrastructure, "Failed to update state in Firestore", e);
            return null;
        }
    }
}