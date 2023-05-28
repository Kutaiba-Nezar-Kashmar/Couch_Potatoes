using System.Text.Json;
using System.Text.Json.Serialization;
using Google.Cloud.Firestore;
using Microsoft.Extensions.Logging;
using User.Application.GetReviewsForMovie.Exceptions;
using User.Domain;
using User.Infrastructure;
using User.Infrastructure.Exceptions;

namespace User.Application.CreateReviewForMovie.Repository;

public class CreateReviewForMovieRepository : ICreateReviewForMovieRepository
{
    private readonly CollectionReference _reference;
    private readonly ILogger _logger;

    public CreateReviewForMovieRepository(ILogger<CreateReviewForMovieRepository> logger)
    {
        _reference = Firestore.Get().Collection("Reviews");
        _logger = logger;
    }

    public async Task CreateForMovie(int movieId, Review review)
    {
        try
        {
            var docRef = _reference.Document(review.ReviewId.ToString());

            var data = new Dictionary<string, object>
            {
                {"Review", review.ToFirestoreReview()}
            };

            await docRef.SetAsync(data);
        }
        catch (Exception e)
        {
            _logger.LogError(LogEvent.Infrastructure, "Failed to store in Firestore", e);
            throw;
        }
    }

    public async Task<IReadOnlyCollection<Review>> GetUsersReviews(string userId)
    {
        try
        {
            var movieReviewsQuery = _reference.WhereEqualTo("Review.UserId", userId);
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
};