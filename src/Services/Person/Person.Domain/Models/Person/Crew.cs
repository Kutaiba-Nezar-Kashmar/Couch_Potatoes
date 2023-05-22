namespace Person.Domain.Models.Person;

public class Crew
{
    public string BackdropPath { get; set; } = default!;
    public string OriginalTitle { get; set; } = default!;
    public string Department { get; set; } = default!;
    public string Job { get; set; } = default!;
    public int MovieId { get; set; }
}