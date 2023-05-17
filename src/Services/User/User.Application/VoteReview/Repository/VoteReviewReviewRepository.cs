using System.Text.Json;
using Google.Cloud.Firestore;
using User.Application.CreateReviewForMovie.Repository;
using User.Domain;
using User.Infrastructure;

namespace User.Application.UpvoteReview.Repository;

public class VoteReviewReviewRepository : IVoteReviewRepository
{
    private readonly CollectionReference _collectionReference;
    private const string CollectionName = "Reviews";

    public VoteReviewReviewRepository()
    {
        _collectionReference = Firestore.Get().Collection(CollectionName);
    }

    public async Task VoteReview(int movieId, Review review, Vote vote)
    {
        var reviewsRef = _collectionReference.Document(movieId.ToString());
        var reviewsSnapshot = await reviewsRef.GetSnapshotAsync();

        var reviewsDto = reviewsSnapshot.ToDictionary()["Reviews"];
        if (reviewsDto == null)
        {
            await reviewsRef.SetAsync(new Dictionary<string, object>()
            {
                {"Reviews", new List<object>()}
            });
            return;
        }

        // NOTE: (mibui 2023-05-17) This is a hack to get it to cast properly
        var domainReviews = JsonSerializer
            .Deserialize<List<FirestoreReviewDto>>(JsonSerializer.Serialize(reviewsDto, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            }))?
            .Select(dto => dto.ToDomainReview()).ToList();


        var reviewToUpvote = domainReviews.First(r => r.ReviewId == review.ReviewId);

        var updatedVotes = reviewToUpvote.Votes.ToList();
        updatedVotes.Add(vote);

        reviewToUpvote.Votes = updatedVotes;

        var updatedReviewsState = domainReviews.Where(r => r.ReviewId != reviewToUpvote.ReviewId).ToList();
        updatedReviewsState.Add(reviewToUpvote);

        await reviewsRef.SetAsync(new Dictionary<string, object>()
        {
            {
                "Reviews", updatedReviewsState.Select(r => r.ToFirestoreReview())
            }
        });
    }
}