namespace MovieInformation.Infrastructure.ControllerDtos.Person;

public class PersonMovieCreditsDto
{
    public IReadOnlyCollection<CastMemberDto> CreditsAsCast { get; set; } =
        default!;

    public IReadOnlyCollection<CrewMemberDto> CreditsAsCrew { get; set; } =
        default!;
}