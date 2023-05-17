using System.Text.Json.Serialization;
using Metrics.Infrastructure.TmdbDtos.PersonDto;

namespace Metrics.Infrastructure.Responses.PersonResponseDtos;

public record GetPersonMovieCreditsResponseDto
(
    [property: JsonPropertyName("cast")] TmdbCastDto[] Cast,
    [property: JsonPropertyName("crew")] TmdbCrewDto[] Crew,
    [property: JsonPropertyName("id")] int Id
);