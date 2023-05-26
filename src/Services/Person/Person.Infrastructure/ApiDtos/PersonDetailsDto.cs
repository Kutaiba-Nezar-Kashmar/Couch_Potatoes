namespace Person.Infrastructure.ApiDtos;

public class PersonDetailsDto
{
    public bool IsAdult { get; set; }
    public IReadOnlyCollection<string> Aliases { get; set; } = default!;
    public string Biography { get; set; } = default!;
    public DateTime? Birthday { get; set; }
    public DateTime? DeathDay { get; set; }
    public string? Gender { get; set; } = default!;
    public string Homepage { get; set; } = default!;
    public string KnownForDepartment { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string PlaceOfBirth { get; set; } = default!;
    public float Popularity { get; set; }
    public string ProfilePath { get; set; } = default!;
}