namespace MovieInformation.Infrastructure.ControllerDtos.Movie.MovieReleaseDates;

public class MovieReleaseDateDetailsDto
{
    public string Certification { get; set; } = default!;
    public string Note { get; set; } = default!;
    public DateTime? ReleaseDate { get; set; }
    public int Type { get; set; }
}