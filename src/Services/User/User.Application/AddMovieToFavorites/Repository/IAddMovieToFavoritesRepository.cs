using User.Domain;

namespace User.Application.AddMovieToFavorites.Repository;

public interface IAddMovieToFavoritesRepository
{
    Task AddMovieToUsersFavorites(CouchPotatoUser user, int movieId);
}
