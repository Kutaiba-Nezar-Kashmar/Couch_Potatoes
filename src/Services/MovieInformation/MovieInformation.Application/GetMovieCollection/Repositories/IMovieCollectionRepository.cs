using MovieInformation.Domain.Models;
using MovieInformation.Domain.Models.Movie;

namespace MovieInformation.Application.GetMovieCollection.Repositories;

public interface IMovieCollectionRepository
{
    Task<MovieCollectionPage> GetMovieCollection(int page,
        string collectionType);
}