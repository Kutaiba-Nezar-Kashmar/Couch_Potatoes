namespace MovieInformation.Domain.Models;

public class MovieCollection
{
    public IReadOnlyCollection<MovieCollectionPage> pages { get; set; }
}