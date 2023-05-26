namespace Search.Domain.Models;

public class MovieSearch
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string PosterPath { get; set; } = default!;
    public DateTime? ReleaseDate { get; set; }
}