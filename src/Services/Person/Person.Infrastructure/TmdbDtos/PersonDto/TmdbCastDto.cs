using System.Text.Json.Serialization;

namespace Person.Infrastructure.TmdbDtos.PersonDto;

public record TmdbCastDto
(
    [property: JsonPropertyName("adult")] bool Adult,
    [property: JsonPropertyName("backdrop_path")]
    string BackdropPath,
    [property: JsonPropertyName("genre_ids")]
    int[] GenreIds,
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
    int VoteCount,
    [property: JsonPropertyName("character")]
    string Character,
    [property: JsonPropertyName("credit_id")]
    string CreditId,
    [property: JsonPropertyName("order")] int Order,
    [property: JsonPropertyName("id")] int Id
);