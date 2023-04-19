using MovieInformation.Domain.Models;
using MovieInformation.Infrastructure.TmbdDto.MovieDto;

namespace MovieInformation.Infrastructure.Util;

public interface IDtoToDomainMapper<TFrom, TTo>
{
    TTo Map(TFrom from);
}

public class MovieCollectionToMovieMapper : IDtoToDomainMapper<MovieCollection, Movie>
{
    public Movie Map(MovieCollection from)
    {
        return new Movie
        {
            Id = from.Id,
            Title = from.Title,
            Summary = from.Overview,
            ImageUri = from.PosterPath,
            BackdropUri = from.BackdropPath,
            TmdbScore = from.VoteAverage,
            TmbdVoteCount = from.VoteCount,
            ReleaseDate = DateTime.Parse(from.ReleaseDate),
            IsForKids = !from.Adult,
            Languages = new List<Language>
            {
                new()
                {
                    Name = from.OriginalLanguage
                }
            }
        };
    }
}