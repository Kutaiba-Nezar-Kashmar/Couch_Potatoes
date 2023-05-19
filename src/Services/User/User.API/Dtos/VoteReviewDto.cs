using Newtonsoft.Json;

namespace User.API.Dtos;

public class VoteReviewDto
{
    // string userId, int movieId, Guid reviewId, VoteDirection direction
    [JsonProperty("userId")]
    public string UserId{ get; set; }

    [JsonProperty("direction")]
    public string Direction{ get; set; }
}