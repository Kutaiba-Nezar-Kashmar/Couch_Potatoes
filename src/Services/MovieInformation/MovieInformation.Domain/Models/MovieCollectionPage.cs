namespace MovieInformation.Domain.Models;

public class MovieCollectionPage
{
    public IReadOnlyCollection<Movie>? Movies { get; set; }
    public required int Page { get; set; }

    public bool IsMissing() => Movies is null || Movies.Count == 0;
}