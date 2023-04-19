using System.Text.Json.Serialization;

namespace MovieInformation.Infrastructure.TmbdDto.PersonDto;

public record PersonDetails
(
    [property: JsonPropertyName("birthday")]
    string Birthday,
    [property: JsonPropertyName("known_for_department")]
    string KnownForDepartment,
    [property: JsonPropertyName("deathday")]
    string Deathday,
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("also_known_as")]
    List<string> AlsoKnownAs,
    [property: JsonPropertyName("gender")] int Gender,
    [property: JsonPropertyName("biography")]
    string Biography,
    [property: JsonPropertyName("popularity")]
    double Popularity,
    [property: JsonPropertyName("place_of_birth")]
    string PlaceOfBirth,
    [property: JsonPropertyName("profile_path")]
    string ProfilePath,
    [property: JsonPropertyName("adult")] bool Adult,
    [property: JsonPropertyName("imdb_id")]
    string ImdbId,
    [property: JsonPropertyName("homepage")]
    string Homepage
);