namespace Person.Domain.Models.Person;

public class Crew
{
    public string BackdropPath { get; set; } = default!;
    public string OriginalTitle { get; set; } = default!;
    public string Department { get; set; } = default!;
    public string Job { get; set; } = default!;
    public int MovieId { get; set; }
    public IReadOnlyCollection<int> GenreIds { get; set; } = default!;
    public string OriginalLanguage { get; set; } = default!;
    public string Overview { get; set; } = default!;
    public double Popularity { get; set; }
    public string PosterPath { get; set; } = default!;
    public DateTime? ReleaseDate { get; set; }
    public string Title { get; set; } = default!;
    public bool HasVideo { get; set; }
    public float VoteAverage { get; set; }
    public int VoteCount { get; set; }
    public string CreditId { get; set; } = default!;
}