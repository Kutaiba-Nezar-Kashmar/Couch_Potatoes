using Metrics.Domain.Models.Person;

namespace Metrics.Application.PersonMetrics.Math;

public class CalculatePersonStatistics : ICalculatePersonStatistics
{
    public PersonStatistics CalculateStatistics(PersonMovieCredits credits)
    {
        var totalNumberOfMovies = CalculateNumberOfMovies(credits);
        var castAverage = CalculateAverageMovieRatingAsCast(credits);
        var crewAverage = CalculateAverageMovieRatingAsCrew(credits);
        var knownFor = CalculateKnownForGenre(credits);

        return new PersonStatistics
        {
            NumberOfMovies = totalNumberOfMovies,
            AverageMoviesRatingsAsACast = castAverage,
            AverageMoviesRatingsAsACrew = crewAverage,
            KnownForGenreId = knownFor
        };
    }

    private static int CalculateNumberOfMovies(PersonMovieCredits credits)
    {
        var uniqueCredits = new HashSet<string>();
        var castCreditCount =
            credits.CreditsAsCast.Count(
                cast => uniqueCredits.Add(cast.CreditId));
        var crewCreditCount =
            credits.CreditsAsCrew.Count(
                crew => uniqueCredits.Add(crew.CreditId));

        return castCreditCount + crewCreditCount;
    }

    private static float CalculateAverageMovieRatingAsCast
    (
        PersonMovieCredits credits
    )
    {
        var castCredit = credits.CreditsAsCast;
        var total = castCredit.Count;
        var sum = castCredit.Sum(cast => cast.VoteAverage);

        return sum / total;
    }

    private static float CalculateAverageMovieRatingAsCrew
    (
        PersonMovieCredits credits
    )
    {
        var castCredit = credits.CreditsAsCrew;
        var total = castCredit.Count;
        var sum = castCredit.Sum(cast => cast.VoteAverage);

        return sum / total;
    }

    private static int CalculateKnownForGenre
    (
        PersonMovieCredits credits
    )
    {
        var count = new Dictionary<int, int>();

        foreach (var cast in credits.CreditsAsCast)
        {
            foreach (var genreId in cast.GenreIds)
            {
                if (count.ContainsKey(genreId))
                {
                    count[genreId]++;
                }
                else
                {
                    count[genreId] = 1;
                }
            }
        }

        return count.OrderByDescending(keyValuePair => keyValuePair.Value)
            .FirstOrDefault().Key;
    }
}