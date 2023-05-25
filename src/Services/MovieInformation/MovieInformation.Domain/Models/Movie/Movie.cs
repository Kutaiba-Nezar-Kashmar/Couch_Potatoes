using MovieInformation.Domain.Models.MovieImages;
using MovieInformation.Domain.Models.MovieVideos;

namespace MovieInformation.Domain.Models.Movie;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string Summary { get; set; } = default!;
    public string ImageUri { get; set; } = default!;
    public string TagLine { get; set; } = default!;
    public string BackdropUri { get; set; } = default!;
    public float TmdbScore { get; set; }
    public int TmdbVoteCount { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int RunTime { get; set; }
    public bool IsForKids { get; set; }

    public string Status { get; set; } = default!;

    public string Homepage { get; set; } = default!;
    public string TrailerUri { get; set; } = default!;
    public long Budget { get; set; }
    public long Revenue { get; set; }
    public IReadOnlyCollection<Keyword> Keywords { get; set; } = default!;
    public IReadOnlyCollection<Language> Languages { get; set; } = default!;
    public IReadOnlyCollection<Genre> Genres { get; set; } = default!;
    public IReadOnlyCollection<MovieImage> Posters { get; set; } = default!;
    public IReadOnlyCollection<MovieImage> Backdrops { get; set; } = default!;
    public IReadOnlyCollection<MovieImage> Logos { get; set; } = default!;
    public IReadOnlyCollection<MovieVideo> Videos { get; set; } = default!;
}