using Google.Cloud.Firestore;
using User.Application.CreateReviewForMovie.Repository;
using User.Domain;
using User.Infrastructure;

namespace User.Application.RemoveMovieFromFavorites.Repository;

public class RemoveMovieFromFavoritesRepository : IRemoveMovieFromFavoritesRepository
{
    private CollectionReference _collectionReference;
    private const string CollectionName = "Users";

    public RemoveMovieFromFavoritesRepository()
    {
        _collectionReference = Firestore.Get().Collection(CollectionName);
    }

    public async Task RemoveMovieFromFavoritesForUser(CouchPotatoUser user, int movieId)
    {
        var userRef = _collectionReference.Document(user.Id);
        FirebaseAuthRepository authRepo = new();
        var userState = await authRepo.GetUserById(user.Id);

        var userHasMovieAsFavorite = userState.FavoriteMovies.Any(i => i == movieId);
        if (!userHasMovieAsFavorite)
        {
            return;
        }

        userState.FavoriteMovies = userState.FavoriteMovies
            .Where(i => i != movieId)
            .ToList();
        
        await userRef.SetAsync(userState.ToFirestoreDto());
    }
}