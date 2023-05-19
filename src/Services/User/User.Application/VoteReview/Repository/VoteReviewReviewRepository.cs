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

    public async Task DeleteVote(int movieId, Guid reviewId, Guid voteId)
    {
        var doc = _collectionReference.Document(movieId.ToString());
        var snapshot = await doc.GetSnapshotAsync();

        if (!snapshot.Exists)
        {
            return;
        }

        var reviews = await GetReviewsForMovie(movieId);

        var review = reviews.First(review => review.ReviewId == reviewId);
        review.Votes = review.Votes.Where(vote => vote.Id != voteId).ToList();

        await doc.SetAsync(new Dictionary<string, object>()
        {
            {"Reviews", reviews.Select(rev => rev.ToFirestoreReview())}
        });
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

        var domainReviews = await GetReviewsForMovie(movieId);

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