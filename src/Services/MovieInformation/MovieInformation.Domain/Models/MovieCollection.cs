namespace MovieInformation.Domain.Models;

public class MovieCollection
{
    public IReadOnlyCollection<MovieCollectionPage> pages { get; set; }
    public static string Popular = "popular";
    public static string Latest = "latest";
    public static string TopRated = "top_rated";
    public static string Upcoming = "upcoming";
}