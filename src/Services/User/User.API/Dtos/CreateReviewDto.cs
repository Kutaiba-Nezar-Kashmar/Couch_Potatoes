using Newtonsoft.Json;

namespace User.API.Dtos;

public class CreateReviewDto
{
    [JsonProperty("userId")]
    public Guid UserId { get; set; }

    [JsonProperty("movieId")]
    public int MovieId { get; set; }

    [JsonProperty("rating")]
    public float Rating { get; set; }

    [JsonProperty("reviewText")]
    public string ReviewText { get; set; }
}