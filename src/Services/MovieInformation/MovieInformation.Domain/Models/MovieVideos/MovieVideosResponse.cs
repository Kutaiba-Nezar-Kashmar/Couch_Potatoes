namespace MovieInformation.Domain.Models.MovieVideos;

public class MovieVideosResponse
{
    public int Id { get; set; }
    public IReadOnlyCollection<MovieVideo> Results { get; set; } = default!;
}