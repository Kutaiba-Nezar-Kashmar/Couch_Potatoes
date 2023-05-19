using System.Text.Json.Serialization;

namespace MovieInformation.Infrastructure.TmbdDto.KeywordsDto;

public record KeywordsDetailsResponseDto
(
     [property: JsonPropertyName("id")] int movieId,
     [property: JsonPropertyName("keywords")]
     IReadOnlyCollection<KeywordResponseDto> keywords
);


public record KeywordResponseDto
(
     [property: JsonPropertyName("id")]
     int Id,
     [property: JsonPropertyName("name")]
     string Name
);