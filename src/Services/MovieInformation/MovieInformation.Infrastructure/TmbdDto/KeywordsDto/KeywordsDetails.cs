using System.Text.Json.Serialization;

namespace MovieInformation.Infrastructure.TmbdDto.KeywordsDto;

public record KeywordsDetails
(
     [property: JsonPropertyName("id")]
     int Id,
     [property: JsonPropertyName("name")]
     string Name
);