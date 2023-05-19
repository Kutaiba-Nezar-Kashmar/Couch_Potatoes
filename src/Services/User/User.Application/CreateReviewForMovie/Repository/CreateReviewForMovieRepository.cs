using System.Text.Json;
using System.Text.Json.Serialization;
using Google.Cloud.Firestore;
using Microsoft.Extensions.Logging;
using User.Domain;
using User.Infrastructure;

namespace User.Application.CreateReviewForMovie.Repository;

public class CreateReviewForMovieRepository : ICreateReviewForMovieRepository
{
    private readonly CollectionReference _reference;
    private readonly ILogger _logger;

    public CreateReviewForMovieRepository(ILogger<CreateReviewForMovieRepository>  logger)
    {
        _reference = Firestore.Get().Collection("Reviews");
        _logger = logger;
    }

    public async Task CreateForMovie(int movieId, Review review)
    {
        try
        {
            var docRef = _reference.Document(movieId.ToString());

            var reviews = await GetReviewsForMovie(movieId);
            var updatedReviewsState = reviews.Concat(new[] {review});

            await docRef.SetAsync(new Dictionary<string, object>()
            {
                {"Reviews", updatedReviewsState.Select(rev => rev.ToFirestoreReview())}
            });
        }
        catch (Exception e)
        {
            _logger.LogError(LogEvent.Infrastructure, "Failed to store in Firestore", e);
            throw;
        }
    }

    public async Task<IReadOnlyCollection<Review>> GetReviewsForMovie(int id)
    {
        try
        {
            var doc = _reference.Document(id.ToString());
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
            _logger.LogError(LogEvent.Infrastructure, "Failed to store in Firestore", e);
            throw;
        }
    }
};