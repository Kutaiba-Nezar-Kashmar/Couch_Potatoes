using System.Text.Json.Serialization;
using Search.Infrastructure.TmdbDtos.MultiSearch;

namespace Search.Infrastructure.Responses;

public record TmdbMultiSearchResponseDto
(
    [property: JsonPropertyName("page")] int Page,
    [property: JsonPropertyName("results")] TmdbMultiSearchResultDto[] Results,
    [property: JsonPropertyName("total_pages")]
    int TotalPages,
    [property: JsonPropertyName("total_results")]
    int TotalResults
);