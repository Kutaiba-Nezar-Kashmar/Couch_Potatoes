using System.Text.Json;
using Google.Apis.Logging;
using Google.Cloud.Firestore;
using Microsoft.Extensions.Logging;
using User.Application.CreateReviewForMovie.Repository;
using User.Application.UpdateReviewForMovie.Repository;
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


    public async Task<Review?> GetReviewById(Guid id)
    {
        var doc = _collectionReference.Document(id.ToString());
        var reviewSnapshot = await doc.GetSnapshotAsync();

        var reviewObject = reviewSnapshot.ToDictionary()["Review"];

        var reviewDto = JsonSerializer.Deserialize<FirestoreReviewDto>(JsonSerializer.Serialize(reviewObject,
            new JsonSerializerOptions {PropertyNameCaseInsensitive = true}));

        return reviewDto switch
        {
            null => null,
            _ => reviewDto.ToDomainReview()
        };
    }

    public async Task<Review?> UpdateReview(int movieId, Guid reviewId, int rating, string reviewText,
        DateTime updateTime)
    {
        var doc = _collectionReference.Document(reviewId.ToString());
        var snapshot = await doc.GetSnapshotAsync();
        if (!snapshot.Exists)
        {
            return null;
        }

        try
        {
            var reviewToUpdate = await GetReviewById(reviewId);
            
            if (reviewToUpdate is null)
            {
                return null;
            }

            reviewToUpdate.ReviewText = reviewText;
            reviewToUpdate.Rating = rating;
            reviewToUpdate.LastUpdatedDate = updateTime;

            await doc.SetAsync(new Dictionary<string, object>
            {
                {"Review", reviewToUpdate.ToFirestoreReview()}
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