namespace MovieInformation.Domain.Models;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Summary { get; set; }
    public string ImageUri { get; set; }
    public string TagLine { get; set; }
    public string BackdropUri { get; set; }
    public float TmdbScore { get; set; }
    public int TmdbVoteCount { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int RunTime { get; set; }
    public bool IsForKids { get; set; }
    public string Status { get; set; } // TODO: make into Enum and find out how to deal with white spaces
    public string Homepage { get; set; }
    public string TrailerUri { get; set; }
    public int Budget { get; set; }
    public int Revenue { get; set; }
    public IEnumerable<Keyword> Keywords { get; set; }
    public IEnumerable<Language> Languages { get; set; }
    public IEnumerable<Genre> Genres { get; set; }
    public IEnumerable<Image> Posters { get; set; }
    public IEnumerable<Image> Backdrops { get; set; }
    public IEnumerable<Image> Logos { get; set; }
}