using Metrics.Application.PersonMetrics.Exceptions;
using Metrics.Domain.Models.Person;

namespace Metrics.Application.PersonMetrics.Calculations;

public class CalculatePersonStatistics : ICalculatePersonStatistics
{
    public int CalculateNumberOfMovies(PersonMovieCredits credits)
    {
        try
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
        catch (Exception e)
        {
            throw new StatisticsException(
                $"Unable to {nameof(CalculateNumberOfMovies)}: {e.Message}", e);
        }
    }

    public float CalculateAverageMovieRatingAsCast
    (
        PersonMovieCredits credits
    )
    {
        try
        {
            var castCredit = credits.CreditsAsCast;
            var total = castCredit.Count;
            var sum = castCredit.Sum(cast => cast.VoteAverage);

            return sum / total;
        }
        catch (Exception e)
        {
            throw new StatisticsException(
                $"Unable to {nameof(CalculateAverageMovieRatingAsCast)}: {e.Message}",
                e);
        }
    }

    public float CalculateAverageMovieRatingAsCrew
    (
        PersonMovieCredits credits
    )
    {
        try
        {
            var castCredit = credits.CreditsAsCrew;
            var total = castCredit.Count;
            var sum = castCredit.Sum(cast => cast.VoteAverage);

            return sum / total;
        }
        catch (Exception e)
        {
            throw new StatisticsException(
                $"Unable to {nameof(CalculateAverageMovieRatingAsCrew)}: {e.Message}",
                e);
        }
    }

    public int CalculateKnownForGenre
    (
        PersonMovieCredits credits
    )
    {
        try
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

            return count
                .FirstOrDefault().Key;
        }
        catch (Exception e)
        {
            throw new StatisticsException(
                $"Unable to {nameof(CalculateKnownForGenre)}: {e.Message}", e);
        }
    }
}