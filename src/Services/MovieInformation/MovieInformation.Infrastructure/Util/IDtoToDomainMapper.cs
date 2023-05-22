using MovieInformation.Domain.Models;
using MovieInformation.Infrastructure.ResponseDtos;
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
            TmdbVoteCount = from.VoteCount,
            ReleaseDate = DateTime.Parse(from.ReleaseDate),
            IsForKids = from.Adult,
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

public class TmdbKeywordsToKeywords : IDtoToDomainMapper<KeywordResponseDto, Keyword>
{
    public Keyword Map(KeywordResponseDto from)
    {
        return new Keyword
        {
            Id = from.Id,
            Name = from.Name
        };
    }
}

public class TmdbImagesToImages : IDtoToDomainMapper<MovieImagesResponseDto, Image>
{
    public Image Map(MovieImagesResponseDto from)
    {
        return new Image
        {
            filePath = from.BackdropImages[0].ToString()
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
            TmdbVoteCount = from.VoteCount,
            ReleaseDate = DateTime.Parse(from.ReleaseDate),
            IsForKids = !from.Adult,
            TagLine = from.Tagline,
            Languages = from.SpokenLanguages.Select(l => new Language
            {
                Code = l.Iso6391,
                Name = l.Name
            }).ToList(),
            Keywords = new List<Keyword>
            {
                new()
                {
                    Name = "fix"
                }
            },
            Genres = from.Genres.Select(g => new Genre
            {
                Id = g.Id,
                Name = g.Name
            }).ToList()
        };
    }
}

public class TmdbPersonMovieCreditToDomainMapper : IDtoToDomainMapper<GetPersonMovieCreditsResponseDto, PersonMovieCredits>
{
    public PersonMovieCredits Map(GetPersonMovieCreditsResponseDto from)
    {
        return new PersonMovieCredits
        {
            CreditsAsCast = from.Cast.Select(c => new CastMember
            {
                IsAdult = c.IsAdult,
                Gender = c.Gender,
                Id = c.Id,
                KnownForDepartment = c.KnownForDepartment,
                Name = c.Name,
                OriginalName = c.OriginalName,
                Popularity = c.Popularity,
                ProfilePath = c.ProfilePath,
                CastId = c.CastId,
                Character = c.Character,
                CreditId = c.CreditId,
                Order = c.Order
            }).ToList(),

            CreditsAsCrew = from.Crew.Select(c => new CrewMember
            {
                IsAdult = c.IsAdult,
                Gender = c.Gender,
                Id = c.Id,
                KnownForDepartment = c.KnownForDepartment,
                Name = c.Name,
                OriginalName = c.OriginalName,
                Popularity = c.Popularity,
                ProfilePath = c.ProfilePath,
                CreditId = c.CreditId,
                Department = c.Department,
                job = c.Job
            }).ToList()
        };
    }
}