using System.Text.Json.Serialization;

namespace MovieInformation.Infrastructure.TmbdDto.PersonDto;

public record TmdbCastDto
(
    [property: JsonPropertyName("adult")] bool IsAdult,
    [property: JsonPropertyName("gender")] int Gender,
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("known_for_department")]
    string KnownForDepartment,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("original_name")]
    string OriginalName,
    [property: JsonPropertyName("popularity")]
    float Popularity,
    [property: JsonPropertyName("profile_path")]
    string ProfilePath,
    [property: JsonPropertyName("cast_id")]
    int CastId,
    [property: JsonPropertyName("character")]
    string Character,
    [property: JsonPropertyName("credit_id")]
    string CreditId,
    [property: JsonPropertyName("order")] int Order
);