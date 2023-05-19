using Google.Cloud.Firestore;
using User.Domain;

namespace User.Application.CreateReviewForMovie.Repository;

[FirestoreData]
public class FirestoreUserDto
{
    [FirestoreProperty]
    public string Id { get; set; }

    [FirestoreProperty]
    public string DisplayName { get; set; }

    [FirestoreProperty]
    public string Email { get; set; }

    [FirestoreProperty]
    public List<int> FavoriteMovies { get; set; } = new List<int>();
}

public static class FirestoreUserExtensions
{
    public static CouchPotatoUser ToDomainUser(this FirestoreUserDto dto)
    {
        return new CouchPotatoUser()
        {
            DisplayName = dto.DisplayName,
            Email = dto.Email,
            FavoriteMovies = dto.FavoriteMovies,
            Id = dto.Id
        };
    }

    public static FirestoreUserDto ToFirestoreDto(this CouchPotatoUser user)
    {
        return new FirestoreUserDto()
        {
            DisplayName = user.DisplayName,
            Email = user.Email,
            FavoriteMovies = user.FavoriteMovies,
            Id = user.Id
        };
    }
}