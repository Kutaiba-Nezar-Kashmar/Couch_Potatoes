using Google.Cloud.Firestore;
using Microsoft.Extensions.Logging;
using User.Application.CreateReviewForMovie.Repository;
using User.Domain;
using User.Infrastructure;

namespace User.Application.RemoveMovieFromFavorites.Repository;

public class RemoveMovieFromFavoritesRepository : IRemoveMovieFromFavoritesRepository
{
    private readonly CollectionReference _collectionReference;
    private const string CollectionName = "Users";
    private readonly IAuthenticationRepository _auth;
    private readonly ILogger _logger;

    public RemoveMovieFromFavoritesRepository(ILogger<RemoveMovieFromFavoritesRepository> logger,
        IAuthenticationRepository auth)
    {
        _collectionReference = Firestore.Get().Collection(CollectionName);
        _auth = auth;
        _logger = logger;
    }

    public async Task RemoveMovieFromFavoritesForUser(CouchPotatoUser user, int movieId)
    {
        try
        {
            var userRef = _collectionReference.Document(user.Id);
            var userState = await _auth.GetUserById(user.Id);

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
        catch (Exception e)
        {
            _logger.LogError(LogEvent.Infrastructure, "Failed to update Firestore state", e);
            throw;
        }
    }
}