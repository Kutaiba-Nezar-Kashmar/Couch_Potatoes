using System.Text.Json.Serialization;
using MovieInformation.Infrastructure.TmbdDto.MovieReleaseDatesDto;

namespace MovieInformation.Infrastructure.ResponseDtos.MovieResponses;

public record TmdbMovieReleaseDatesResponseDto
(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("results")]
    TmdbReleaseDateDto[] Results
);