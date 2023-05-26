using MovieInformation.Domain.Models.Movie;
using MovieInformation.Domain.Models.MovieImages;
using MovieInformation.Domain.Models.MovieReleaseDates;
using MovieInformation.Domain.Models.MovieVideos;
using MovieInformation.Domain.Models.Person;
using MovieInformation.Domain.Models.ProductionCompanie;
using MovieInformation.Infrastructure.ControllerDtos.Images;
using MovieInformation.Infrastructure.ControllerDtos.Movie;
using MovieInformation.Infrastructure.ControllerDtos.Movie.MovieReleaseDates;
using MovieInformation.Infrastructure.ControllerDtos.Movie.ProductionCompanies;
using MovieInformation.Infrastructure.ControllerDtos.Videos;
using MovieInformation.Infrastructure.ResponseDtos.MediaResponses;
using MovieInformation.Infrastructure.ResponseDtos.MovieResponses;
using MovieInformation.Infrastructure.ResponseDtos.PersonResponses;
using MovieInformation.Infrastructure.TmbdDto.KeywordsDto;
using MovieInformation.Infrastructure.TmbdDto.MovieDto;

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
            }).ToList(),
            ProductionCompanies = from.ProductionCompanies.Select(p =>
                new MovieProductionCompany
                {
                    Id = p.Id,
                    Name = p.Name,
                    LogoPath = p.LogoPath,
                    OriginCountry = p.OriginCountry
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
                Job = c.Job
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
                Width = p.Width,
                Lang = p.Lang
            }).ToList(),
            Backdrops = from.Backdrops.Select(b => new MovieImageDto
            {
                Height = b.Height,
                FilePath = b.FilePath,
                Width = b.Width,
                Lang = b.Lang
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
                Name = v.Name,
                Type = v.Type
            }).ToList(),
            ReleaseDates = from.ReleaseDates.Select(r =>
                new MovieReleaseDatesDto
                {
                    Lang = r.Lang,
                    ReleaseDatesDetails = r.ReleaseDatesDetails.Select(d =>
                        new MovieReleaseDateDetailsDto
                        {
                            Note = d.Note,
                            Type = d.Type,
                            ReleaseDate = d.ReleaseDate,
                            Certification = d.Certification
                        }).ToList()
                }).ToList(),
            ProductionCompanies = from.ProductionCompanies.Select(p =>
                new ProductionCompaniesDto
                {
                    Name = p.Name,
                    LogoPath = p.LogoPath,
                    OriginCountry = p.OriginCountry,
                    Id = p.Id
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

public class TmdbMovieReleaseDateToDomainMapper : IDtoToDomainMapper<
    TmdbMovieReleaseDatesResponseDto, MovieReleaseDateResponse>
{
    public MovieReleaseDateResponse Map(TmdbMovieReleaseDatesResponseDto from)
    {
        return new MovieReleaseDateResponse
        {
            Id = from.Id,
            Results = from.Results.Select(r => new MovieReleaseDate
            {
                Lang = r.Lang,
                ReleaseDatesDetails = r.ReleaseDatesDetails.Select(d =>
                    new MovieReleaseDatesDetails
                    {
                        Type = d.Type,
                        Lang = d.Lang,
                        ReleaseDate =
                            DateTimeParser.ParseDateTime(d.ReleaseDate),
                        Certification = d.Certification,
                        Descriptors = d.Descriptors,
                        Note = d.Note
                    }).ToList()
            }).ToList()
        };
    }
}