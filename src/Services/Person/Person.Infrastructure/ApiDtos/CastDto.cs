namespace Person.Infrastructure.ApiDtos;

public class CastDto
{
    public string PosterPath { get; set; } = default!;
    public string Title { get; set; } = default!;
    public int MovieId { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public string Character { get; set; } = default!;

}