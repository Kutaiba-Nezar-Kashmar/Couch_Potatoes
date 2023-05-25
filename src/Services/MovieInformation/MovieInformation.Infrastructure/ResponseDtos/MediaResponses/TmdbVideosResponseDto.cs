using System.Text.Json.Serialization;
using MovieInformation.Infrastructure.TmbdDto.MovieVideoDto;

namespace MovieInformation.Infrastructure.ResponseDtos.MediaResponses;

public record TmdbVideosResponseDto
(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("results")]
    TmdbVideoDto[] Results
);