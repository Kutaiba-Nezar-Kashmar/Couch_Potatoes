using System.Text.Json.Serialization;
using Person.Infrastructure.TmdbDtos.PersonDto;

namespace Person.Infrastructure.Responses.PersonResponseDtos;

public record GetPersonMovieCreditsResponseDto
(
    [property: JsonPropertyName("cast")] TmdbCastDto[] Cast,
    [property: JsonPropertyName("crew")] TmdbCrewDto[] Crew,
    [property: JsonPropertyName("id")] int Id
);