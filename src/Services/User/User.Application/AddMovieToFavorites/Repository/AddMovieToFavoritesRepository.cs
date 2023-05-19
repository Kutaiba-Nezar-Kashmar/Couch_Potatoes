using Google.Cloud.Firestore;
using Microsoft.Extensions.Logging;
using User.Application.CreateReviewForMovie.Repository;
using User.Domain;
using User.Infrastructure;

namespace User.Application.AddMovieToFavorites.Repository;

public class AddMovieToFavoritesRepository : IAddMovieToFavoritesRepository
{
    private readonly CollectionReference _collectionReference;
    private const string CollectionName = "Users";
    private readonly ILogger _logger;

    public AddMovieToFavoritesRepository(ILogger<AddMovieToFavoritesRepository> logger)
    {
        _collectionReference = Firestore.Get().Collection(CollectionName);
        _logger = logger;
    }

    public async Task AddMovieToUsersFavorites(CouchPotatoUser user, int movieId)
    {
        try
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
        catch (Exception e)
        {
            _logger.LogError(LogEvent.Infrastructure, "Failed to store in firestore", e);
            throw;
        }
    }
}