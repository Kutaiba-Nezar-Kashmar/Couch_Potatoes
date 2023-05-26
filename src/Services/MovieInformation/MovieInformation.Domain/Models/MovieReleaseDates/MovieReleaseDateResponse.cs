namespace MovieInformation.Domain.Models.MovieReleaseDates;

public class MovieReleaseDateResponse
{
    public int Id { get; set; }

    public IReadOnlyCollection<MovieReleaseDate> Results { get; set; } =
        default!;
}