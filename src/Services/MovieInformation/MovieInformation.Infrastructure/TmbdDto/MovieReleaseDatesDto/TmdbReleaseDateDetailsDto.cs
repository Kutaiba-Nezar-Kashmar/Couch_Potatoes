using System.Text.Json.Serialization;

namespace MovieInformation.Infrastructure.TmbdDto.MovieReleaseDatesDto;

public record TmdbReleaseDateDetailsDto
(
    [property: JsonPropertyName("certification")]
    string Certification,
    [property: JsonPropertyName("descriptors")]
    string[] Descriptors,
    [property: JsonPropertyName("iso_639_1")]
    string Lang,
    [property: JsonPropertyName("note")] string Note,
    [property: JsonPropertyName("release_date")]
    string ReleaseDate,
    [property: JsonPropertyName("type")] int Type
);