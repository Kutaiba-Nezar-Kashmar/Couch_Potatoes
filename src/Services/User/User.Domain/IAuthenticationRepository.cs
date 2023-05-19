namespace User.Infrastructure;

public interface IAuthenticationRepository
{
    public Task<Domain.CouchPotatoUser?> GetUserById(string id);
}