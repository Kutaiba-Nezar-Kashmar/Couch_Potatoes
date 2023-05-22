using System.Text.Json.Serialization;

namespace MovieInformation.Infrastructure.TmbdDto.PersonDto;

public record TmdbCrewDto
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
    [property: JsonPropertyName("credit_id")]
    string CreditId,
    [property: JsonPropertyName("department")]
    string Department,
    [property: JsonPropertyName("job")] string Job
);