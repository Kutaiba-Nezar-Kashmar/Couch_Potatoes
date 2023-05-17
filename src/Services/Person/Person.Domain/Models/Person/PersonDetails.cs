namespace Person.Domain.Models.Person;

public class PersonDetails
{
    public bool IsAdult { get; set; }
    public IReadOnlyCollection<string> Aliases { get; set; } = default!;
    public string Biography { get; set; } = default!;
    public DateTime Birthday { get; set; }
    public DateTime DeathDay { get; set; }
    public Gender Gender { get; set; }
    public string Homepage { get; set; } = default!;
    public int Id { get; set; }
    public string ImdbId { get; set; } = default!;
    public string KnownForDepartment { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string PlaceOfBirth { get; set; } = default!;
    public float Popularity { get; set; }
    public string ProfilePath { get; set; } = default!;
}

public enum Gender
{
    Unspecified = 0,
    Female = 1,
    Male = 2
}