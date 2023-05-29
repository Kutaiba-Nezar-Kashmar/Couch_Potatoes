namespace User.Infrastructure;

public interface IAuthenticationRepository
{
    public Task<Domain.CouchPotatoUser?> GetUserById(string id);
    public Task<Domain.CouchPotatoUser?> UpdateUserProfile(string userId, string newDisplayName, string newAvatarUri);
}