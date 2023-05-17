using System.Text.Json.Serialization;

namespace Metrics.Infrastructure.TmdbDtos.GenreDto;

public record TmdbGenreDto
(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name
);