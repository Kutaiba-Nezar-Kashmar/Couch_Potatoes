namespace Metrics.Domain.Models.Person;

public class Crew
{
    public IReadOnlyCollection<int> GenreIds { get; set; }
    public float VoteAverage { get; set; }
    public int VoteCount { get; set; }
}