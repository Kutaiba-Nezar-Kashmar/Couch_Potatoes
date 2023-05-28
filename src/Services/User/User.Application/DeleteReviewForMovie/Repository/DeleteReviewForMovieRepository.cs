using System.Text.Json;
using Google.Cloud.Firestore;
using Microsoft.Extensions.Logging;
using User.Application.CreateReviewForMovie.Repository;
using User.Application.DeleteReview.Repository;
using User.Domain;
using User.Infrastructure;

namespace User.Application.DeleteReviewForMovie.Repository;

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
    
    public async Task<Review?> GetReviewById(Guid reviewId)
    {
        var doc = _collectionReference.Document(reviewId.ToString());
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

    public async Task DeleteReview(int movieId, Guid reviewId)
    {
        try
        {
            var doc = _collectionReference.Document(reviewId.ToString());
            await doc.DeleteAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(LogEvent.Infrastructure, $"Failed to delete review in Firestore", e);
            throw;
        }
    }
}