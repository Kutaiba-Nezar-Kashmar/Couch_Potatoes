namespace Search.Domain.Models;

public class MovieSearch
{
    public bool IsAdult { get; set; }
    public string BackdropOath { get; set; } = default!;
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string OriginalLanguage { get; set; } = default!;
    public string OriginalTitle { get; set; } = default!;
    public string Overview { get; set; } = default!;
    public string MediaType { get; set; } = default!;
    public string PosterPath { get; set; } = default!;
    public DateTime? ReleaseDate { get; set; }
    public IReadOnlyCollection<int> GenreIds { get; set; } = default!;
    public float Popularity { get; set; }
    public bool HasVideo { get; set; }
    public float VoteAverage { get; set; }
    public int VoteCount { get; set; }
}