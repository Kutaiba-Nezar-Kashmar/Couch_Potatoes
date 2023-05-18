using System.Text.Json.Serialization;

namespace Person.Infrastructure.TmdbDtos.PersonDetailsDto;

public record TmdbPersonDetailsDto
(
    [property: JsonPropertyName("adult")] bool Adult, 
    [property: JsonPropertyName("also_known_as")] string[] AlsoKnownAs, 
    [property: JsonPropertyName("biography")] string Biography, 
    [property: JsonPropertyName("birthday")] string Birthday, 
    [property: JsonPropertyName("deathday")] string DeathDay, 
    [property: JsonPropertyName("gender")] int Gender, 
    [property: JsonPropertyName("homepage")] string Homepage, 
    [property: JsonPropertyName("id")] int Id, 
    [property: JsonPropertyName("imdb_id")] string ImdbId, 
    [property: JsonPropertyName("known_for_department")] string KnownForDepartment, 
    [property: JsonPropertyName("name")] string Name, 
    [property: JsonPropertyName("place_of_birth")] string PlaceOfBirth, 
    [property: JsonPropertyName("popularity")] float Popularity, 
    [property: JsonPropertyName("ProfilePath")] string ProfilePath
);