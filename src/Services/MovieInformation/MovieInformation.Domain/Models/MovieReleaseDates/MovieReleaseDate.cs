namespace MovieInformation.Domain.Models.MovieReleaseDates;

public class MovieReleaseDate
{
    public string Lang { get; set; } = default!;

    public IReadOnlyCollection<MovieReleaseDatesDetails> ReleaseDatesDetails
    {
        get;
        set;
    } = default!;
}