using Newtonsoft.Json;

namespace User.API.Dtos;

public class VoteDto
{
    [JsonProperty("direction")]
    public string Direction { get; set; }
    [JsonProperty("id")]
    public Guid Id { get; set; }
    [JsonProperty("userId")]
    public string UserId { get; set; }
}