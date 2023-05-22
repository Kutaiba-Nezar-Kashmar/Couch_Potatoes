using Person.Domain.Models.Person;
using Person.Infrastructure.Responses.PersonResponseDtos;

namespace Person.Infrastructure.Util.Mappers;

public class TmdpPersonMovieCreditToDomainMapper : IDtoToDomainMapper<
    GetPersonMovieCreditsResponseDto, PersonMovieCredits>
{
    public PersonMovieCredits Map(GetPersonMovieCreditsResponseDto from)
    {
        return new PersonMovieCredits
        {
            CreditsAsCast = from.Cast.Select(c => new Cast
            {
                BackdropPath = c.BackdropPath,
                OriginalTitle = c.OriginalTitle,
                MovieId = c.Id,
                IsAdult = c.Adult,
                Overview = c.Overview,
                GenreIds = c.GenreIds,
                Popularity = c.Popularity,
                PosterPath = c.PosterPath,
                ReleaseDate = DateTimeParser.ParseDateTime(c.ReleaseDate),
                Title = c.Title,
                HasVideo = c.Video,
                VoteAverage = c.VoteAverage,
                VoteCount = c.VoteCount,
                Character = c.Character,
                CreditId = c.CreditId,
                Order = c.Order
            }).ToList(),

            CreditsAsCrew = from.Crew.Select(c => new Crew
            {
                BackdropPath = c.BackdropPath,
                OriginalTitle = c.OriginalTitle,
                Department = c.Department,
                Job = c.Job,
                MovieId = c.Id,
                GenreIds = c.GenreIds,
                OriginalLanguage = c.OriginalLanguage,
                Overview = c.Overview,
                Popularity = c.Popularity,
                PosterPath = c.PosterPath,
                ReleaseDate = DateTimeParser.ParseDateTime(c.ReleaseDate),
                Title = c.Title,
                HasVideo = c.Video,
                VoteAverage = c.VoteAverage,
                VoteCount = c.VoteCount,
                CreditId = c.CreditId
            }).ToList()
        };
    }
}