namespace MovieInformation.Domain.Models.MovieReleaseDates;

public class MovieReleaseDatesDetails
{
    public string Certification { get; set; } = default!;
    public IReadOnlyCollection<string> Descriptors { get; set; } = default!;
    public string Lang { get; set; } = default!;
    public string Note { get; set; } = default!;
    public DateTime? ReleaseDate { get; set; }
    public int Type { get; set; }
}