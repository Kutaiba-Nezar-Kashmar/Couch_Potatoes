namespace Metrics.Domain.Models.Person;

public class Crew
{
    public IReadOnlyCollection<int> GenreIds { get; set; } = default!;
    public float VoteAverage { get; set; }
    public int VoteCount { get; set; }
    public string CreditId { get; set; } = default!;
}