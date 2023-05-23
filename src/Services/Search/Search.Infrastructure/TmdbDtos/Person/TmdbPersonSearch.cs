using System.Text.Json.Serialization;

namespace Search.Infrastructure.TmdbDtos.Person;

public record TmdbPersonSearch
(
    [property: JsonPropertyName("adult")] bool IsAdult,
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("original_name")]
    string OriginalName,
    [property: JsonPropertyName("media_type")]
    string MediaType,
    [property: JsonPropertyName("popularity")]
    float Popularity,
    [property: JsonPropertyName("gender")] int Gender,
    [property: JsonPropertyName("known_for_department")]
    string KnownForDepartment,
    [property: JsonPropertyName("profile_path")]
    string ProfilePath,
    [property: JsonPropertyName("known_for")]
    object[] KnownFor
);