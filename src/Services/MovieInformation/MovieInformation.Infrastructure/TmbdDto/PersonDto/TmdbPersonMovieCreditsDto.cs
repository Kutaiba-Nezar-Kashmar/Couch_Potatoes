using System.Text.Json.Serialization;

namespace MovieInformation.Infrastructure.TmbdDto.PersonDto;

public record TmdbPersonMovieCreditsDto
(
    [property: JsonPropertyName("cast")] TmdbCastDto[] Cast,
    [property: JsonPropertyName("crew")] TmdbCrewDto[] Crew
);