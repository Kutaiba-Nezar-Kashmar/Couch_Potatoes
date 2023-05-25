using System.Text.Json.Serialization;
using MovieInformation.Infrastructure.TmbdDto.ImageDto;

namespace MovieInformation.Infrastructure.ResponseDtos;

public record TmdbImagesResponseDto
(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("backdrops")]
    TmdbBackdropsDto[] Backdrops,
    [property: JsonPropertyName("logos")]
    TmdbBackdropsDto[] Logos,
    [property: JsonPropertyName("posters")]
    TmdbBackdropsDto[] Posters
);