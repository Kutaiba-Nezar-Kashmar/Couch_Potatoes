namespace MovieInformation.Domain.Models.Person;

public class PersonMovieCredits
{
    public IReadOnlyCollection<CastMember> CreditsAsCast { get; set; } =
        default!;

    public IReadOnlyCollection<CrewMember> CreditsAsCrew { get; set; } =
        default!;
}