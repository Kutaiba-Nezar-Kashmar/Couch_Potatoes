using System.Text.Json.Serialization;

namespace MovieInformation.Infrastructure.TmbdDto.MovieReleaseDatesDto;

public record TmdbReleaseDateDto
(
    [property: JsonPropertyName("iso_3166_1")]
    string Lang,
    [property: JsonPropertyName("release_dates")]
    TmdbReleaseDateDetailsDto[] ReleaseDatesDetails
);