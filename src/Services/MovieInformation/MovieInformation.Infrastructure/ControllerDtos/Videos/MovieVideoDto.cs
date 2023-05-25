namespace MovieInformation.Infrastructure.ControllerDtos.Videos;

public class MovieVideoDto
{
    public string Name { get; set; } = default!;
    public string Key { get; set; } = default!;
    public DateTime? PublishedAt { get; set; }
    public string Id { get; set; } = default!;
    public string Type { get; set; } = default!;
}