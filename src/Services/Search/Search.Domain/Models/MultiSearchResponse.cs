namespace Search.Domain.Models;

public class MultiSearchResponse
{
    public IReadOnlyCollection<PersonSearch> People { get; set; } = default!;
    public IReadOnlyCollection<MovieSearch> Movies { get; set; } = default!;
}