using System.Text.Json.Serialization;

namespace Metrics.Infrastructure.TmdbDtos.PersonDto;

public record TmdbPersonMovieCreditsDto
(
    [property: JsonPropertyName("cast")] TmdbCastDto[] Cast,
    [property: JsonPropertyName("crew")] TmdbCrewDto[] Crew
);