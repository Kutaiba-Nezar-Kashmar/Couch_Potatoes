namespace MovieInformation.Infrastructure.ResponseDtos;

public class ReadMovieCollectionPageDto
{
    public IReadOnlyCollection<ReadDetailedMovieDto>? Movies { get; set; }
    public required int Page { get; set; }

    public bool IsMissing() => Movies is null || Movies.Count == 0;
}