using Newtonsoft.Json;

namespace User.API.Dtos;

public class ReviewDto
{
    [JsonProperty("reviewId")]
    public string ReviewId{ get; set; }
    
    [JsonProperty("userId")]
    public string UserId { get; set; }
    [JsonProperty("movieId")]
    public int MovieId { get; set; }
    [JsonProperty("rating")]
    public float Rating { get; set; }
    
    [JsonProperty("reviewText")]
    public string ReviewText { get; set; }
    [JsonProperty("creationDate")]
    public DateTime CreationDate { get; set; }
    [JsonProperty("votes")]
    public IReadOnlyCollection<VoteDto> Votes { get; set; } = new List<VoteDto>();

    public bool IsValid()
    {
        return MovieId > 0 && Rating is >= 0 and <= 10 && UserId != null && CreationDate <= DateTime.UtcNow;
    }
}