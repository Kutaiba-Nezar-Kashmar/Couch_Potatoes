namespace Metrics.Domain.Models.Person;

public class Cast
{
    public IReadOnlyCollection<int> GenreIds { get; set; }
    public float VoteAverage { get; set; }
    public int VoteCount { get; set; }
}