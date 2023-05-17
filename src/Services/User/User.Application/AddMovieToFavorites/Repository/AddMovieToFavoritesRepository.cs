using Google.Cloud.Firestore;
using User.Application.CreateReviewForMovie.Repository;
using User.Domain;
using User.Infrastructure;

namespace User.Application.AddMovieToFavorites.Repository;

public class AddMovieToFavoritesRepository : IAddMovieToFavoritesRepository
{
    private readonly CollectionReference _collectionReference;
    private const string CollectionName = "Users";

    public AddMovieToFavoritesRepository()
    {
        _collectionReference = Firestore.Get().Collection(CollectionName);
    }

    public async Task AddMovieToUsersFavorites(CouchPotatoUser user, int movieId)
    {
        var userRef = _collectionReference.Document(user.Id);
        FirebaseAuthRepository authRepo = new();
        var userState = await authRepo.GetUserById(user.Id);

        var userHasAlreadyMovieAsFavorite = userState.FavoriteMovies.Any(i => i == movieId);

        if (userHasAlreadyMovieAsFavorite)
        {
            return;
        }
        
        userState.FavoriteMovies.Add(movieId);
        await userRef.SetAsync(userState.ToFirestoreDto());
    }
}