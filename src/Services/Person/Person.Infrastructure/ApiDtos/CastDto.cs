namespace Person.Infrastructure.ApiDtos;

public class CastDto
{
    public string BackdropPath { get; set; } = default!;
    public string OriginalTitle { get; set; } = default!;
    public int MovieId { get; set; }
}