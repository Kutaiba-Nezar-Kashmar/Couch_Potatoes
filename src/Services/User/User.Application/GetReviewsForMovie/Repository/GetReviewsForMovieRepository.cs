using System.Text.Json;
using System.Text.Json.Serialization;
using Google.Cloud.Firestore;
using Microsoft.Extensions.Logging;
using User.Application.CreateReviewForMovie.Repository;
using User.Domain;
using User.Infrastructure;

namespace User.Application.GetReviewsForMovie.Repository;

public class GetReviewsForMovieRepository : IGetReviewsForMovieRepository
{
    private readonly CollectionReference _reference;
    private readonly ILogger _logger;

    public GetReviewsForMovieRepository(ILogger<GetReviewsForMovieRepository> logger)
    {
        _reference = Firestore.Get().Collection("Reviews");
        _logger = logger;
    }

    public async Task<IReadOnlyCollection<Review>> Get(int movieId)
    {
        try
        {
            var doc = _reference.Document(movieId.ToString());
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
            _logger.LogError(LogEvent.Infrastructure, "Failed to retrieve data from Firestore", e);
            throw;
        }
    }
}