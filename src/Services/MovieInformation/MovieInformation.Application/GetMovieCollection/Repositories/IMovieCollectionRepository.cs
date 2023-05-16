using MovieInformation.Domain.Models;

namespace MovieInformation.Application.GetMovieCollection.Repositories;

public interface IMovieCollectionRepository
{
    Task<MovieCollectionPage> GetMovieCollection(int page,
        string collectionType);
}