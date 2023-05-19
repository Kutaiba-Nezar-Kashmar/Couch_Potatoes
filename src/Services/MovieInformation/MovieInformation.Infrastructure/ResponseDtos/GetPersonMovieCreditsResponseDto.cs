using System.Text.Json.Serialization;
using MovieInformation.Infrastructure.TmbdDto.PersonDto;

namespace MovieInformation.Infrastructure.ResponseDtos;

public record GetPersonMovieCreditsResponseDto
(
    [property: JsonPropertyName("cast")] TmdbCastDto[] Cast,
    [property: JsonPropertyName("crew")] TmdbCrewDto[] Crew,
    [property: JsonPropertyName("id")] int Id
);