using System.Text.Json;
using Google.Cloud.Firestore;
using Microsoft.Extensions.Logging;
using User.Application.CreateReviewForMovie.Repository;
using User.Application.GetReviewsForMovie.Exceptions;
using User.Domain;
using User.Infrastructure;
using User.Infrastructure.Exceptions;

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
            var movieReviewsQuery = _collectionReference.WhereEqualTo("Review.UserId", userId);
            var movieReviewsQuerySnapshot = await movieReviewsQuery.GetSnapshotAsync();

            var reviewObjects = movieReviewsQuerySnapshot.Select(docSnapshot => docSnapshot.ToDictionary()["Review"]);

            var reviewsDtos = JsonSerializer.Deserialize<IEnumerable<FirestoreReviewDto>>(
                JsonSerializer.Serialize(reviewObjects,
                    new JsonSerializerOptions {PropertyNameCaseInsensitive = true}
                )
            );

            if (reviewsDtos is null)
            {
                throw new InfrastructureException("Failed to retrieve user's review from Firestore");
            }

            return reviewsDtos.Select(dto => dto.ToDomainReview()).ToList();
        }
        catch (Exception e) when (e is not InfrastructureException)
        {
            _logger.LogError(LogEvent.Infrastructure, e, $"Failed to get reviews from Firestore: {e}");
            throw;
        }
    }
}