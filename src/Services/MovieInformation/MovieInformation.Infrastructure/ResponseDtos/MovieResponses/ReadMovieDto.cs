namespace MovieInformation.Infrastructure.ResponseDtos.MovieResponses;

public class ReadMovieDto
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string Summary { get; set; } = default!;
    public string ImageUri { get; set; } = default!;
    public string BackdropUri { get; set; } = default!;
    public float TmdbScore { get; set; }
    public int TmdbVoteCount { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public bool IsForKids { get; set; }
}