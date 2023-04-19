using System.Text.Json.Serialization;

namespace MovieInformation.Infrastructure.TmbdDto.CreditsDto;

public record Media<T>
(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("original_name")]
    string OriginalName,
    [property: JsonPropertyName("character")]
    string Character,
    [property: JsonPropertyName("episodes")]
    List<T> Episodes,
    [property: JsonPropertyName("seasons")]
    List<Season> Seasons
);

public record Person
(
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("id")] int Id
);

public record Season
(
    [property: JsonPropertyName("air_date")]
    string AirDate,
    [property: JsonPropertyName("poster_path")]
    string PosterPath,
    [property: JsonPropertyName("season_number")]
    int SeasonNumber
);

public record CreditsDetail<T>
(
    [property: JsonPropertyName("credit_type")]
    string CreditType,
    [property: JsonPropertyName("department")]
    string Department,
    [property: JsonPropertyName("job")] string Job,
    [property: JsonPropertyName("media")] Media<T> Media,
    [property: JsonPropertyName("media_type")]
    string MediaType,
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("person")] Person Person
);