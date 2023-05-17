using System.Text.Json;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using User.Application.CreateReviewForMovie.Repository;
using User.Domain;

namespace User.Infrastructure;

public class FirebaseAuthRepository : IAuthenticationRepository
{
    public async Task<CouchPotatoUser?> GetUserById(string id)
    {
        try
        {
            var defaultInstance = FirebaseAuth.DefaultInstance;
            if (defaultInstance == null)
            {
                Firestore.CreateFirestoreApp();
            }

            UserRecord firebaseUser = await FirebaseAuth.DefaultInstance.GetUserAsync(id);
            var domainUser = new CouchPotatoUser()
            {
                Email = firebaseUser.Email,
                DisplayName = firebaseUser.DisplayName,
                Id = firebaseUser.Uid,
                FavoriteMovies = new List<int>()
            };

            var userRef = Firestore.Get().Collection("Users").Document(id);
            var snapshot = await userRef.GetSnapshotAsync();

            if (!snapshot.Exists)
            {
                await userRef.SetAsync(domainUser.ToFirestoreDto());
            }

            domainUser.FavoriteMovies = (await GetFavoriteMovies(id)).ToList();
            return domainUser;
        }
        catch (FirebaseAuthException e)
        {
            return null;
        }
    }

    private async Task<IReadOnlyCollection<int>> GetFavoriteMovies(string userId)
    {
        var collection = Firestore.Get().Collection("Users");
        var userRef = collection.Document(userId);
        var userSnapshot = await userRef.GetSnapshotAsync();
        var userFavoriteMovies =
            JsonSerializer.Deserialize<List<int>>(
                JsonSerializer.Serialize(userSnapshot.ToDictionary()["FavoriteMovies"],
                    new JsonSerializerOptions() {PropertyNameCaseInsensitive = true}));

        return !userSnapshot.Exists ? new List<int>() : userFavoriteMovies ?? new List<int>();
    }
}