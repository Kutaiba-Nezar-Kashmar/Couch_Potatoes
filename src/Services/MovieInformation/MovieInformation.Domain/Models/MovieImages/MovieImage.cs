namespace MovieInformation.Domain.Models.MovieImages;

public class MovieImage
{
    public float AspectRatio { get; set; }
    public float Height { get; set; }
    public string Lang { get; set; } = default!;
    public string FilePath { get; set; } = default!;
    public float VoteAverage { get; set; }
    public int VoteCount { get; set; }
    public int Width { get; set; }
}