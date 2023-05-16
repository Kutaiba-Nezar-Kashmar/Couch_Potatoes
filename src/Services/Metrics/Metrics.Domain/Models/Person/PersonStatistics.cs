namespace Metrics.Domain.Models.Person;

public class PersonStatistics
{
    public int NumberOfMovies { get; set; }
    public float AverageMoviesRatingsAsACast { get; set; }
    public float AverageMoviesRatingsAsACrew { get; set; }
    public IReadOnlyCollection<int> KnownForGenreIds { get; set; }
}