using User.Domain;

namespace User.Application.RemoveMovieFromFavorites.Repository;

public interface IRemoveMovieFromFavoritesRepository
{
    Task RemoveMovieFromFavoritesForUser(CouchPotatoUser user, int movieId);
}