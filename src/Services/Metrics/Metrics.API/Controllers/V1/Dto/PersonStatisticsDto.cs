namespace Metrics.API.Controllers.V1.Dto;

public class PersonStatisticsDto
{
    public int NumberOfMovies { get; set; }
    public float AverageMoviesRatingsAsACast { get; set; }
    public float AverageMoviesRatingsAsACrew { get; set; }
    public string KnownForgenre { get; set; }
}