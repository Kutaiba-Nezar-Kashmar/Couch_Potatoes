namespace MovieInformation.Infrastructure.ControllerDtos.Movie.
    MovieReleaseDates;

public class MovieReleaseDateResponseDto
{
    public int Id { get; set; }

    public IReadOnlyCollection<MovieReleaseDatesDto> Results { get; set; } =
        default!;
}