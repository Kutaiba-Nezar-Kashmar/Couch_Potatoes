namespace Metrics.Domain.Models.Person;

public class PersonMovieCredits
{
    public IReadOnlyCollection<Cast> CreditsAsCast { get; set; }
    public IReadOnlyCollection<Crew> CreditsAsCrew { get; set; }
}