using System.Text.Json;
using System.Text.Json.Serialization;
using Google.Cloud.Firestore;
using User.Domain;
using User.Infrastructure;

namespace User.Application.CreateReviewForMovie.Repository;

public class CreateReviewForMovieRepository : ICreateReviewForMovieRepository
{
    private readonly CollectionReference _reference;

    public CreateReviewForMovieRepository()
    {
        _reference = Firestore.Get().Collection("Reviews");
    }

    public async Task CreateForMovie(int movieId, Review review)
    {
        var docRef = _reference.Document(movieId.ToString());
        var updatedReviewsState = new Dictionary<string, object>()
        {
            {"Reviews", new List<FirestoreReviewDto>() {review.ToFirestoreReview()}}
        };

        await docRef.SetAsync(updatedReviewsState, SetOptions.MergeAll);
    }

    public async Task<IReadOnlyCollection<Review>> GetReviewsForMovie(int id)
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
};