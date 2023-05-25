using System.Text.Json.Serialization;

namespace MovieInformation.Infrastructure.TmbdDto.MovieVideoDto;

public record TmdbVideoDto
(
    [property: JsonPropertyName("iso_639_1")]
    string LangLower,
    [property: JsonPropertyName("iso_3166_1")]
    string LangUpper,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("key")] string Key,
    [property: JsonPropertyName("site")] string Site,
    [property: JsonPropertyName("size")] int Size,
    [property: JsonPropertyName("type")] string Type,
    [property: JsonPropertyName("official")]
    bool IsOfficial,
    [property: JsonPropertyName("published_at")]
    string PublishedAt,
    [property: JsonPropertyName("id")] string Id
);