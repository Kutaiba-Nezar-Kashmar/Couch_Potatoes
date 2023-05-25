using MovieInformation.Domain.Models;
using MovieInformation.Domain.Models.MovieImages;
using MovieInformation.Domain.Models.MovieVideos;
using MovieInformation.Infrastructure.ControllerDtos.Images;
using MovieInformation.Infrastructure.ControllerDtos.Movie;
using MovieInformation.Infrastructure.ControllerDtos.Videos;
using MovieInformation.Infrastructure.ResponseDtos;
using MovieInformation.Infrastructure.TmbdDto.KeywordsDto;
using MovieInformation.Infrastructure.TmbdDto.MovieDto;
using MovieInformation.Infrastructure.TmbdDto.ResponseDto;

namespace MovieInformation.Infrastructure.Util;

public interface IDtoToDomainMapper<TFrom, TTo>
{
    TTo Map(TFrom from);
}

public class
    TmdbMovieCollectionToMovie : IDtoToDomainMapper<TmdbMovieCollection, Movie>
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
            ReleaseDate = !string.IsNullOrEmpty(from.ReleaseDate)
                ? DateTime.Parse(from.ReleaseDate)
                : DateTime.MinValue,
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

public class
    TmdbKeywordsToKeywords : IDtoToDomainMapper<KeywordResponseDto, Keyword>
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

public class
    TmdbImagesToImages : IDtoToDomainMapper<MovieImagesResponseDto, Image>
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
            ReleaseDate = !string.IsNullOrEmpty(from.ReleaseDate)
                ? DateTime.Parse(from.ReleaseDate)
                : DateTime.MinValue,
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

public class TmdbPersonMovieCreditToDomainMapper : IDtoToDomainMapper<
    GetPersonMovieCreditsResponseDto, PersonMovieCredits>
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

public class TmdbImagesDtoToDomainMapper : IDtoToDomainMapper<
    TmdbImagesResponseDto, MovieImagesResponse>
{
    public MovieImagesResponse Map(TmdbImagesResponseDto from)
    {
        return new MovieImagesResponse
        {
            Backdrops = from.Backdrops.Select(b => new MovieImage
            {
                AspectRatio = b.AspectRatio,
                Height = b.Height,
                Lang = b.Lang,
                FilePath = b.FilePath,
                VoteAverage = b.VoteAverage,
                VoteCount = b.VoteCount,
                Width = b.Width
            }).ToList(),
            Logos = from.Logos.Select(l => new MovieImage
            {
                AspectRatio = l.AspectRatio,
                Height = l.Height,
                Lang = l.Lang,
                FilePath = l.FilePath,
                VoteAverage = l.VoteAverage,
                VoteCount = l.VoteCount,
                Width = l.Width
            }).ToList(),
            Posters = from.Posters.Select(p => new MovieImage
            {
                AspectRatio = p.AspectRatio,
                Height = p.Height,
                Lang = p.Lang,
                FilePath = p.FilePath,
                VoteAverage = p.VoteAverage,
                VoteCount = p.VoteCount,
                Width = p.Width
            }).ToList()
        };
    }
}

public class DomainMovieImagesToControllerImagesDtoMapper :
    IDtoToDomainMapper<MovieImagesResponse, ControllerMovieImagesDto>
{
    public ControllerMovieImagesDto Map(MovieImagesResponse from)
    {
        return new ControllerMovieImagesDto
        {
            Posters = from.Posters.Select(p => new MovieImageDto
            {
                Width = p.Width,
                FilePath = p.FilePath,
                Height = p.Height
            }).ToList()
        };
    }
}

public class
    DomainToReadDetailedMovieDtoMapper : IDtoToDomainMapper<Movie,
        MovieControllerDto>
{
    public MovieControllerDto Map(Movie from)
    {
        return new MovieControllerDto
        {
            Id = from.Id,
            Title = from.Title,
            Summary = from.Summary,
            Budget = from.Budget,
            RunTime = from.RunTime,
            Genres = from.Genres.Select(g => new ReadGenreDto
            {
                Id = g.Id,
                Name = g.Name
            }).ToList(),
            Keywords = from.Keywords.Select(k => new ReadKeywordDto
            {
                Id = k.Id,
                Name = k.Name
            }).ToList(),
            Homepage = from.Homepage,
            Revenue = from.Revenue,
            TmdbVoteCount = from.TmdbVoteCount,
            Posters = from.Posters.Select(p => new MovieImageDto
            {
                Height = p.Height,
                FilePath = p.FilePath,
                Width = p.Width
            }).ToList(),
            Languages = from.Languages.Select(l => new ReadLanguageDto
            {
                Code = l.Code,
                Name = l.Name
            }).ToList(),
            BackdropUri = from.BackdropUri,
            ImageUri = from.ImageUri,
            ReleaseDate = from.ReleaseDate,
            TmdbScore = from.TmdbScore,
            IsForKids = from.IsForKids,
            Status = from.Status,
            TagLine = from.TagLine,
            TrailerUri = from.TrailerUri,
            Videos = from.Videos.Select(v => new MovieVideoDto
            {
                PublishedAt = v.PublishedAt,
                Id = v.Id,
                Key = v.Key,
                Name = v.Name
            }).ToList()
        };
    }
}

public class TmdbVideoToDomainMapper : IDtoToDomainMapper<TmdbVideosResponseDto,
    MovieVideosResponse>
{
    public MovieVideosResponse Map(TmdbVideosResponseDto from)
    {
        return new MovieVideosResponse
        {
            Id = from.Id,
            Results = from.Results.Select(r => new MovieVideo
            {
                Type = r.Type,
                Id = r.Id,
                Name = r.Name,
                Key = r.Key,
                Site = r.Site,
                Size = r.Size,
                IsOfficial = r.IsOfficial,
                LangLower = r.LangLower,
                LangUpper = r.LangUpper,
                PublishedAt = DateTimeParser.ParseDateTime(r.PublishedAt)
            }).ToList()
        };
    }
}