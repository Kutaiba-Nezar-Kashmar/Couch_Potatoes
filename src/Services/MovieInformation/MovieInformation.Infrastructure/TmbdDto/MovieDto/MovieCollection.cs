using System.Text.Json.Serialization;

namespace MovieInformation.Infrastructure.TmbdDto.MovieDto;

public record MovieCollection
(
    [property: JsonPropertyName("adult")] bool Adult,
    [property: JsonPropertyName("backdrop_path")]
    string BackdropPath,
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("original_language")]
    string OriginalLanguage,
    [property: JsonPropertyName("original_title")]
    string OriginalTitle,
    [property: JsonPropertyName("overview")]
    string Overview,
    [property: JsonPropertyName("popularity")]
    double Popularity,
    [property: JsonPropertyName("poster_path")]
    string PosterPath,
    [property: JsonPropertyName("release_date")]
    string ReleaseDate,
    [property: JsonPropertyName("title")] string Title,
    [property: JsonPropertyName("video")] bool Video,
    [property: JsonPropertyName("vote_average")]
    float VoteAverage,
    [property: JsonPropertyName("vote_count")]
    int VoteCount
);