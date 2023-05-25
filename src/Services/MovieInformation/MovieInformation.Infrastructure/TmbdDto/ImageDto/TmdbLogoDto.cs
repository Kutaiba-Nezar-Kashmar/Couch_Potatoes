using System.Text.Json.Serialization;

namespace MovieInformation.Infrastructure.TmbdDto.ImageDto;

public record TmdbLogoDto
(
    [property: JsonPropertyName("aspect_ratio")]
    float AspectRatio,
    [property: JsonPropertyName("height")] float Height,
    [property: JsonPropertyName("iso_639_1")]
    string Lang,
    [property: JsonPropertyName("file_path")]
    string FilePath,
    [property: JsonPropertyName("vote_average")]
    float VoteAverage,
    [property: JsonPropertyName("vote_count")]
    int VoteCount,
    [property: JsonPropertyName("width")] int Width
);