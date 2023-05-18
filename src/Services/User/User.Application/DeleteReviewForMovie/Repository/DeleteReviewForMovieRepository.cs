using System.Text.Json;
using Google.Cloud.Firestore;
using Microsoft.Extensions.Logging;
using User.Application.CreateReviewForMovie.Repository;
using User.Domain;
using User.Infrastructure;

namespace User.Application.DeleteReview.Repository;

public class DeleteReviewForMovieRepository : IDeleteReviewForMovieRepository
{
    private readonly ILogger _logger;
    private readonly CollectionReference _collectionReference;
    private const string CollectionName = "Reviews";

    public DeleteReviewForMovieRepository(ILogger<DeleteReviewForMovieRepository> logger)
    {
        _logger = logger;
        _collectionReference = Firestore.Get().Collection(CollectionName);
    }

    public async Task<IReadOnlyCollection<Review>> GetReviewsForMovie(int movieId)
    {
        try
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
        catch (Exception e)
        {
            _logger.LogError(LogEvent.Infrastructure, $"Failed to retrieve from Firestore", e);
            throw;
        }
    }

    public async Task DeleteReview(int movieId, Guid reviewId)
    {
        try
        {
            var doc = _collectionReference.Document(movieId.ToString());
            var snapshot = await doc.GetSnapshotAsync();

            if (!snapshot.Exists)
            {
                return;
            }

            var reviews = await GetReviewsForMovie(movieId);

            var updatedReviewState = reviews.Where(review => review.ReviewId != reviewId);

            await doc.SetAsync(new Dictionary<string, object>()
            {
                {"Reviews", updatedReviewState}
            });
        }
        catch (Exception e)
        {
            _logger.LogError(LogEvent.Infrastructure, $"Failed to delete review in Firestore", e);
            throw;
        }
    }
}