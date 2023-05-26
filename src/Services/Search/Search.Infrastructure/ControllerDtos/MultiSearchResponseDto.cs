namespace Search.Infrastructure.ControllerDtos;

public class MultiSearchResponseDto
{
    public IReadOnlyCollection<PersonSearchDto> People { get; set; } = default!;
    public IReadOnlyCollection<MovieSearchDto> Movies { get; set; } = default!;
}