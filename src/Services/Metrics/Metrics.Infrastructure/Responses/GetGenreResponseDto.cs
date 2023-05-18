using System.Text.Json.Serialization;
using Metrics.Infrastructure.TmdbDtos.GenreDto;

namespace Metrics.Infrastructure.Responses;

public record GetGenreResponseDto
(
    [property: JsonPropertyName("genres")] TmdbGenreDto[] Genres
);