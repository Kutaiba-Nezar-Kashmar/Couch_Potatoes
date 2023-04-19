using System.Text.Json.Serialization;
using MovieInformation.Infrastructure.TmbdDto.MovieDto;

namespace MovieInformation.Infrastructure.TmbdDto.ResponseDto;

public record GetMovieCollectionResponseDto
(
    [property: JsonPropertyName("page")]
    int Page,
    [property: JsonPropertyName("results")]
    IReadOnlyCollection<MovieCollection> Result,
    [property: JsonPropertyName("total_results")]
    int TotalResults,
    [property: JsonPropertyName("total_pages")]
    int TotalPages
);

