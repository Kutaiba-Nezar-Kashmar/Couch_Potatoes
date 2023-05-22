namespace Person.Infrastructure.ApiDtos;

public class PersonMovieCreditsDto
{
    public IReadOnlyCollection<CastDto> CreditsAsCast { get; set; }
    public IReadOnlyCollection<CrewDto> CreditsAsCrew { get; set; }
}