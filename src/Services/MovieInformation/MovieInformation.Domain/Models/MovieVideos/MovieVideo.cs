namespace MovieInformation.Domain.Models.MovieVideos;

public class MovieVideo
{
    public string LangLower { get; set; } = default!;
    public string LangUpper { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Key { get; set; } = default!;
    public string Site { get; set; } = default!;
    public int Size { get; set; }
    public string Type { get; set; } = default!;
    public bool IsOfficial { get; set; }
    public DateTime? PublishedAt { get; set; }
    public string Id { get; set; } = default!;
}