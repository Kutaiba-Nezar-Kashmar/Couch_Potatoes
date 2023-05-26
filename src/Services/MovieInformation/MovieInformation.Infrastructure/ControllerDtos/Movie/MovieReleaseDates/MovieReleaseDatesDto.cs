namespace MovieInformation.Infrastructure.ControllerDtos.Movie.
    MovieReleaseDates;

public class MovieReleaseDatesDto
{
    public string Lang { get; set; } = default!;

    public IReadOnlyCollection<MovieReleaseDateDetailsDto> ReleaseDatesDetails
    {
        get;
        set;
    } = default!;
}