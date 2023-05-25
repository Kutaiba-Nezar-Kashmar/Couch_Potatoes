namespace MovieInformation.Infrastructure.ControllerDtos.Videos;

public class ControllerMovieVideoDto
{
    public IReadOnlyCollection<MovieVideoDto> Results { get; set; } = default!;
}