namespace Metrics.Domain.Models.Person;

public class PersonStatistics
{
    public int NumberOfMovies { get; set; }
    public float AverageMoviesRatingsAsACast { get; set; }
    public float AverageMoviesRatingsAsACrew { get; set; }
    public string KnownForGenre { get; set; } = default;
}