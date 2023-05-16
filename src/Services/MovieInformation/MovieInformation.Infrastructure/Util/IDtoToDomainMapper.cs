using MovieInformation.Domain.Models;
using MovieInformation.Infrastructure.TmbdDto.KeywordsDto;
using MovieInformation.Infrastructure.TmbdDto.MovieDto;
using MovieInformation.Infrastructure.TmbdDto.ResponseDto;

namespace MovieInformation.Infrastructure.Util;

public interface IDtoToDomainMapper<TFrom, TTo>
{
    TTo Map(TFrom from);
}

public class TmdbMovieCollectionToMovie : IDtoToDomainMapper<TmdbMovieCollection, Movie>
{
    public Movie Map(TmdbMovieCollection from)
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

public class TmdbKeywordsToKeywords : IDtoToDomainMapper<KeywordsDetails, Keyword>
{
    public Keyword Map(KeywordsDetails from)
    {
        return new Keyword
        {
            Id = from.Id,
            Name = from.Name
        };
    }
}
public class TmdbMovieToMovie : IDtoToDomainMapper<MovieDetail, Movie>
{
    public Movie Map(MovieDetail from)
    {
        return new Movie
        {
            Id = from.Id,
            Title = from.Title,
            Summary = from.Overview,
            ImageUri = from.PosterPath,
            Budget = from.Budget,
            RunTime = from.Runtime,
            BackdropUri = from.BackdropPath,
            TmdbScore = from.VoteAverage,
            Status = from.Status,
            Homepage = from.Homepage,
            Revenue = from.Revenue,
            TmbdVoteCount = from.VoteCount,
            ReleaseDate = DateTime.Parse(from.ReleaseDate),
            IsForKids = from.Adult,
            Languages = new List<Language>
            {
                new()
                {
                    Name = from.OriginalLanguage
                }
            },
            Keywords = new List<Keyword>
            {
                new()
                {
                    Name = "fix"
                }
            },
            Genres = new List<Genre>
            {
                new ()
                {
                    
                }
            }
        };
    }
}