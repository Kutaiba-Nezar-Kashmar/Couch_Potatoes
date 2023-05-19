namespace MovieInformation.Domain.Models;

public class PersonMovieCredits
{
    public IReadOnlyCollection<CastMember> CreditsAsCast { get; set; }
    public IReadOnlyCollection<CrewMember> CreditsAsCrew { get; set; }
}