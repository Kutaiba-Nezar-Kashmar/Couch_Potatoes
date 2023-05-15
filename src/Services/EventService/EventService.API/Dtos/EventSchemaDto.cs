using Newtonsoft.Json;

namespace EventService.API.Dtos;

public class EventSchemaDto
{
    [JsonProperty("service")]
    public string Service { get; set; }

    [JsonProperty("content")]
    public ContentSchemaDto Content { get; set; }

    [JsonProperty("routingKey")]
    public string Routingkey { get; set; }
}

public class ContentSchemaDto
{
    [JsonProperty("properties")]
    public IReadOnlyCollection<SchemaPropertyDto> Properties { get; set; }
}

public class SchemaPropertyDto
{
    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("properties")]
    public IReadOnlyCollection<SchemaPropertyDto> Properties { get; set; }
}