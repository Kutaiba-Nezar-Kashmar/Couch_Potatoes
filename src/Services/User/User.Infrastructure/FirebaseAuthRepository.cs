using System.Text.Json;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Logging;
using User.Application.CreateReviewForMovie.Repository;
using User.Domain;

namespace User.Infrastructure;

public class FirebaseAuthRepository : IAuthenticationRepository
{
    private readonly ILogger _logger;

    public FirebaseAuthRepository(ILogger<FirebaseAuthRepository>? logger = null)
    {
        _logger = logger;
    }

    public async Task<CouchPotatoUser> UpdateUserProfile(string userId, string newDisplayName, string newAvatarUri)
    {
        try
        {
            var defaultInstance = FirebaseAuth.DefaultInstance;
            if (defaultInstance == null)
            {
                Firestore.CreateFirestoreApp();
            }

            UserRecord firebaseUser = await FirebaseAuth.DefaultInstance.GetUserAsync(userId);
            var newUserState = new UserRecordArgs
            {
                Email = firebaseUser.Email,
                DisplayName = newDisplayName,
                Uid = firebaseUser.Uid,
                PhotoUrl = newAvatarUri,
                Disabled = firebaseUser.Disabled,
                EmailVerified = firebaseUser.EmailVerified,
                PhoneNumber = firebaseUser.PhoneNumber
            };

            UserRecord updatedUser = await FirebaseAuth.DefaultInstance.UpdateUserAsync(newUserState);
            return await AppendFavoriteMoviesToDomainUser(FirebaseUserAsDomainUser(updatedUser));
        }
        catch (FirebaseAuthException e)
        {
            _logger.LogError(LogEvent.Infrastructure, "Failed to retrieve user from Firestore", e);
            return null;
        }
    }

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
            var domainUser = await AppendFavoriteMoviesToDomainUser(FirebaseUserAsDomainUser(firebaseUser));
            
            return domainUser;
        }
        catch (FirebaseAuthException e)
        {
            _logger.LogError(LogEvent.Infrastructure, "Failed to retrieve user from Firestore", e);
            return null;
        }
    }

    private async Task<CouchPotatoUser> AppendFavoriteMoviesToDomainUser(CouchPotatoUser domainUser)
    {
        var userRef = Firestore.Get().Collection("Users").Document(domainUser.Id);
        var snapshot = await userRef.GetSnapshotAsync();

        if (!snapshot.Exists)
        {
            await userRef.SetAsync(domainUser.ToFirestoreDto());
        }

        domainUser.FavoriteMovies = (await GetFavoriteMovies(domainUser.Id)).ToList();
        return domainUser;
    }

    private async Task<IReadOnlyCollection<int>> GetFavoriteMovies(string userId)
    {
        try
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
        catch (Exception e)
        {
            _logger.LogError(LogEvent.Infrastructure, "Failed to retrieve favorite movies from Firestore", e);
            throw;
        }
    }

    private CouchPotatoUser FirebaseUserAsDomainUser(UserRecord firebaseUser)
    {
        return new CouchPotatoUser()
        {
            Email = firebaseUser.Email,
            DisplayName = firebaseUser.DisplayName,
            Id = firebaseUser.Uid,
            FavoriteMovies = new List<int>(),
            AvatarUri = firebaseUser.PhotoUrl,
            LastSignInTimestamp = firebaseUser.UserMetaData.LastSignInTimestamp,
            CreatedTimestamp = firebaseUser.UserMetaData.CreationTimestamp
        };
    }
}