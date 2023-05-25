namespace MovieInformation.Infrastructure.ResponseDtos.MovieResponses;

public class ReadMovieCollectionDto
{
    public IReadOnlyCollection<ReadMovieDto> Collection { get; set; } =
        default!;

    public IReadOnlyCollection<int> MissingPages { get; set; } = default!;
}