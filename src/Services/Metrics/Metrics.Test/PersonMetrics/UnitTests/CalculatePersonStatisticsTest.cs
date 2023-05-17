using Metrics.Application.PersonMetrics.Calculations;
using Metrics.Application.PersonMetrics.Exceptions;
using Metrics.Domain.Models.Person;

namespace Metrics.Test.PersonMetrics.UnitTests;

public class CalculatePersonStatisticsTest
{
    private ICalculatePersonStatistics _calculatePersonStatistics;

    [SetUp]
    public void Setup()
    {
        _calculatePersonStatistics = new CalculatePersonStatistics();
    }

    [Test]
    public void
        FetchTotalNumberOfPersonMovies_AsCastAndCrew_ReturnsCorrectNumberOfMovies()
    {
        // Arrange
        var credits = new PersonMovieCredits
        {
            CreditsAsCast = new[]
            {
                new Cast
                {
                    CreditId = "1"
                },
                new Cast
                {
                    CreditId = "2"
                }
            },
            CreditsAsCrew = new[]
            {
                new Crew
                {
                    CreditId = "3"
                }
            }
        };

        var totalCount =
            credits.CreditsAsCast.Count + credits.CreditsAsCrew.Count;

        // Act
        var statistics =
            _calculatePersonStatistics.CalculateNumberOfMovies(credits);

        // Assert
        Assert.That(totalCount, Is.EqualTo(statistics));
    }

    [Test]
    public void
        CalculateAveragePersonMovieRating_AsCast_ReturnsCorrectAverageNumber()
    {
        // Arrange
        var credits = new PersonMovieCredits
        {
            CreditsAsCast = new[]
            {
                new Cast
                {
                    VoteAverage = 3.5f
                },
                new Cast
                {
                    VoteAverage = 1.5f
                }
            }
        };

        var count = credits.CreditsAsCast.Count;
        var sum = credits.CreditsAsCast.Sum(cast => cast.VoteAverage);
        var average = sum / count;

        // Act
        var statistics =
            _calculatePersonStatistics.CalculateAverageMovieRatingAsCast(
                credits);

        // Assert
        Assert.That(average, Is.EqualTo(statistics));
    }

    [Test]
    public void
        CalculateAveragePersonMovieRating_AsCrew_ReturnsCorrectAverageNumber()
    {
        // Arrange
        var credits = new PersonMovieCredits
        {
            CreditsAsCrew = new[]
            {
                new Crew
                {
                    VoteAverage = 3.5f
                },
                new Crew
                {
                    VoteAverage = 1.5f
                }
            }
        };

        var count = credits.CreditsAsCrew.Count;
        var sum = credits.CreditsAsCrew.Sum(cast => cast.VoteAverage);
        var average = sum / count;

        // Act
        var statistics =
            _calculatePersonStatistics.CalculateAverageMovieRatingAsCrew(
                credits);

        // Assert
        Assert.That(average, Is.EqualTo(statistics));
    }

    [TestCase(3)]
    public void
        FindingKnownForGenre_ForPersonMovie_ReturnsTheMostDominateGenreId(
            int dominateId)
    {
        // arrange 
        var credits = new PersonMovieCredits
        {
            CreditsAsCast = new[]
            {
                new Cast
                {
                    GenreIds = new[]
                    {
                        1,
                        2,
                        dominateId
                    }
                },
                new Cast
                {
                    GenreIds = new[]
                    {
                        4,
                        6,
                        dominateId
                    }
                },
                new Cast
                {
                    GenreIds = new[]
                    {
                        8,
                        2,
                        dominateId
                    }
                }
            }
        };

        // Act
        var knownFor =
            _calculatePersonStatistics.CalculateKnownForGenre(credits);

        // Assert
        Assert.That(dominateId, Is.EqualTo(knownFor));
    }

    [Test]
    public void
        CalculateStatistics_WithNullCredits_ThrowsStatisticsException()
    {
        // Assert
        Assert.Throws<StatisticsException>(() => _calculatePersonStatistics.CalculateNumberOfMovies(null));
        Assert.Throws<StatisticsException>(() => _calculatePersonStatistics.CalculateAverageMovieRatingAsCast(null));
        Assert.Throws<StatisticsException>(() => _calculatePersonStatistics.CalculateAverageMovieRatingAsCrew(null));
        Assert.Throws<StatisticsException>(() => _calculatePersonStatistics.CalculateKnownForGenre(null));
    }
}