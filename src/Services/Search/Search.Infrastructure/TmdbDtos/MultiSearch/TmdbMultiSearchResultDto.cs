using System.Text.Json.Serialization;

namespace Search.Infrastructure.TmdbDtos.MultiSearch;

public record TmdbMultiSearchResultDto
(
    [property: JsonPropertyName("adult")] bool IsAdult,
    [property: JsonPropertyName("backdrop_path")]
    string BackdropOath,
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("title")] string Title,
    [property: JsonPropertyName("original_language")]
    string OriginalLanguage,
    [property: JsonPropertyName("original_title")]
    string OriginalTitle,
    [property: JsonPropertyName("overview")]
    string Overview,
    [property: JsonPropertyName("poster_path")]
    string PosterPath,
    [property: JsonPropertyName("media_type")]
    string MediaType,
    [property: JsonPropertyName("genre_ids")]
    int[] GenreIds,
    [property: JsonPropertyName("popularity")]
    float Popularity,
    [property: JsonPropertyName("release_date")]
    string ReleaseDate,
    [property: JsonPropertyName("video")] bool HasVideo,
    [property: JsonPropertyName("vote_average")]
    float VoteAverage,
    [property: JsonPropertyName("vote_count")]
    int VoteCount,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("original_name")]
    string OriginalName,
    [property: JsonPropertyName("gender")] int Gender,
    [property: JsonPropertyName("known_for_department")]
    string KnownForDepartment,
    [property: JsonPropertyName("profile_path")]
    string ProfilePath,
    [property: JsonPropertyName("known_for")]
    object[] KnownFor
);