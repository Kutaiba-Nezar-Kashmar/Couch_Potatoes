using System.Text.Json;
using Google.Cloud.Firestore;
using Microsoft.Extensions.Logging;
using User.Application.CreateReviewForMovie.Repository;
using User.Application.GetReviewsForMovie.Exceptions;
using User.Application.UpvoteReview.Repository;
using User.Domain;
using User.Infrastructure;

namespace User.Application.VoteReview.Repository;

public class VoteReviewRepository : IVoteReviewRepository
{
    private readonly ILogger _logger;
    private readonly CollectionReference _collectionReference;
    private const string CollectionName = "Reviews";

    public VoteReviewRepository(ILogger<VoteReviewRepository> logger)
    {
        _collectionReference = Firestore.Get().Collection(CollectionName);
        _logger = logger;
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

    public async Task<IReadOnlyCollection<Review>> GetReviewsForMovie(int movieId)
    {
        try
        {
            var movieReviewsQuery = _collectionReference.WhereEqualTo("MovieId", movieId);
            var movieReviewsQuerySnapshot = await movieReviewsQuery.GetSnapshotAsync();

            var reviewObjects = movieReviewsQuerySnapshot.Select(docSnapshot => docSnapshot.ToDictionary()["Review"]);

            var reviewsDtos = JsonSerializer.Deserialize<IEnumerable<FirestoreReviewDto>>(
                JsonSerializer.Serialize(reviewObjects,
                    new JsonSerializerOptions {PropertyNameCaseInsensitive = true}
                )
            );

            if (reviewsDtos is null)
            {
                throw new FailedToRetrieveReviewsException(movieId);
            }

            return reviewsDtos.Select(dto => dto.ToDomainReview()).ToList();
        }
        catch (Exception e)
        {
            _logger.LogError(LogEvent.Infrastructure, "Failed to store in Firestore", e);
            throw;
        }
    }
    public async Task DeleteVote(int movieId, Guid reviewId, Guid voteId)
    {
        var doc = _collectionReference.Document(reviewId.ToString());
        var review = await GetReviewById(reviewId);
     
        review!.Votes = review.Votes.Where(vote => vote.Id !=  voteId).ToList();
        
        await doc.SetAsync(new Dictionary<string, object>
        {
            {"Review", review.ToFirestoreReview()}
        });
    }


    public async Task VoteReview(int movieId, Review review, Vote vote)
    {
        var reviewsRef = _collectionReference.Document(review.ReviewId.ToString());

        var reviewToUpvote = await GetReviewById(review.ReviewId);
        var updatedVotes = reviewToUpvote!.Votes.Append(vote);

        reviewToUpvote.Votes = updatedVotes.ToList();

        await reviewsRef.SetAsync(new Dictionary<string, object>
        {
            {
                "Review", reviewToUpvote.ToFirestoreReview()
            }
        });
    }
}