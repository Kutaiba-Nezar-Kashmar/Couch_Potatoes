namespace MovieInformation.Domain.Models.Movie;

public class MovieCollectionPage
{
    public IReadOnlyCollection<Models.Movie.Movie>? Movies { get; set; }
    public required int Page { get; set; }

    public bool IsMissing() => Movies is null || Movies.Count == 0;
}