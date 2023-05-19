using System.Text.Json.Serialization;

namespace MovieInformation.Infrastructure.TmbdDto.MovieDto;

public record MovieDetailGenre
(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name
);

public record MovieDetailProductionCompany
(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("logo_path")]
    string LogoPath,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("origin_country")]
    string OriginCountry
);

public record MovieDetailProductionCountry
(
    [property: JsonPropertyName("iso_3166_1")]
    string Iso31661,
    [property: JsonPropertyName("name")] string Name
);

public record MovieDetail
(
    [property: JsonPropertyName("adult")] bool Adult,
    [property: JsonPropertyName("backdrop_path")]
    string BackdropPath,
    [property: JsonPropertyName("belongs_to_collection")]
    object BelongsToCollection,
    [property: JsonPropertyName("budget")] int Budget,
    [property: JsonPropertyName("genres")] List<MovieDetailGenre> Genres,
    [property: JsonPropertyName("homepage")]
    string Homepage,
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("imdb_id")]
    string ImdbId,
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
    [property: JsonPropertyName("production_companies")]
    List<MovieDetailProductionCompany> ProductionCompanies,
    [property: JsonPropertyName("production_countries")]
    List<MovieDetailProductionCountry> ProductionCountries,
    [property: JsonPropertyName("release_date")]
    string ReleaseDate,
    [property: JsonPropertyName("revenue")]
    int Revenue,
    [property: JsonPropertyName("runtime")]
    int Runtime,
    [property: JsonPropertyName("spoken_languages")]
    List<MovieDetailSpokenLanguage> SpokenLanguages,
    [property: JsonPropertyName("status")] string Status,
    [property: JsonPropertyName("tagline")]
    string Tagline,
    [property: JsonPropertyName("title")] string Title,
    [property: JsonPropertyName("video")] bool Video,
    [property: JsonPropertyName("vote_average")]
    float VoteAverage,
    [property: JsonPropertyName("vote_count")]
    int VoteCount
);

public record MovieImagesResponseDto
(
    [property: JsonPropertyName("id")] int id,
    [property: JsonPropertyName("backdrops")]
    List<BackdropImageResponseDto> BackdropImages,
    [property: JsonPropertyName("logos")]
    List<LogoResponseDto> Logos, 
    [property: JsonPropertyName("posters")]
    List<PosterResponseDto> Posters
);

public record PosterResponseDto
    ();

public record BackdropImageResponseDto
(
    [property: JsonPropertyName("aspect_ratio")] float aspectRatio,
    [property: JsonPropertyName("height")] int height, 
    [property: JsonPropertyName("iso_639_1")] string iso,
    [property: JsonPropertyName("file_path")] string filePath,
    [property: JsonPropertyName("vote_average")] float voteAverage,
    [property: JsonPropertyName("vote_count")] int voteCount,
    [property: JsonPropertyName("width")] int width
);

public record LogoResponseDto
    ();

    public record MovieDetailSpokenLanguage
(
    [property: JsonPropertyName("iso_639_1")]
    string Iso6391,
    [property: JsonPropertyName("name")] string Name
);