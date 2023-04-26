namespace MovieInformation.Infrastructure.ResponseDtos;

public class ReadMovieDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Summary { get; set; }
    public string ImageUri { get; set; }
    public string BackdropUri { get; set; }
    public float TmdbScore { get; set; }
    public int TmbdVoteCount { get; set; }
    public DateTime ReleaseDate { get; set; }
    public bool IsForKids { get; set; }
}