namespace Metrics.Domain.Models.Person;

public class PersonMovieCredits
{
    public IReadOnlyCollection<Cast> CreditsAsCast { get; set; } = default!;
    public IReadOnlyCollection<Crew> CreditsAsCrew { get; set; } = default!;
}