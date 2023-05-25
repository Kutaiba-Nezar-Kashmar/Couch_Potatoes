namespace MovieInformation.Infrastructure.ControllerDtos.Images;

public class ControllerMovieImagesDto
{
    public IReadOnlyCollection<MovieImageDto> Posters { get; set; } =
        default!;
}