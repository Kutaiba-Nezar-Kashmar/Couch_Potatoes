namespace MovieInformation.Infrastructure.ResponseDtos;

public class ReadMovieCollectionDto
{
    public IReadOnlyCollection<ReadMovieDto> Collection{ get; set; }
    public IReadOnlyCollection<int> MissingPages{ get; set; }
}