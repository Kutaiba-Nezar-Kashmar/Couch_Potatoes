using Newtonsoft.Json;

namespace User.API.Dtos;

public class DeleteReviewDto
{
    [JsonProperty("userId")]
    public string UserId { get; set; }
}