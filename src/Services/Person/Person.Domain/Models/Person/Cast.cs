namespace Person.Domain.Models.Person;

public class Cast
{
    public string BackdropPath { get; set; } = default!;
    public string OriginalTitle { get; set; } = default!;
    public int MovieId { get; set; }
    public bool IsAdult { get; set; }
    public string Overview { get; set; } = default!;
    public IReadOnlyCollection<int> GenreIds { get; set; } = default!;
    public double Popularity { get; set; }
    public string PosterPath { get; set; } = default!;
    public DateTime? ReleaseDate { get; set; }
    public string Title { get; set; } = default!;
    public bool HasVideo { get; set; }
    public float VoteAverage { get; set; }
    public int VoteCount { get; set; }
    public string Character { get; set; } = default!;
    public string CreditId { get; set; } = default!;
    public int Order { get; set; }
}