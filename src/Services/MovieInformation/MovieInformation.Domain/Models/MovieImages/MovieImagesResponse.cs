namespace MovieInformation.Domain.Models.MovieImages;

public class MovieImagesResponse
{
    public IReadOnlyCollection<MovieImage> Backdrops { get; set; } = default!;
    public IReadOnlyCollection<MovieImage> Logos { get; set; } = default!;
    public IReadOnlyCollection<MovieImage> Posters { get; set; } = default!;
}