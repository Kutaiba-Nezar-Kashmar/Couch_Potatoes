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
}