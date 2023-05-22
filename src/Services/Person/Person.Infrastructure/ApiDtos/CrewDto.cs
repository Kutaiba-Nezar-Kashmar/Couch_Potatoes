namespace Person.Infrastructure.ApiDtos;

public class CrewDto
{
    public string PosterPath { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string Department { get; set; } = default!;
    public string Job { get; set; } = default!;
    public int MovieId { get; set; }
    public DateTime? ReleaseDate { get; set; }
}