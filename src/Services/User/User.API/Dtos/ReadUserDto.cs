using Newtonsoft.Json;

namespace User.API.Dtos;

public class ReadUserDto
{
    [JsonProperty("id")]
    public string Id { get; set; }
    [JsonProperty("displayName")]
    public string DisplayName { get; set; }
    [JsonProperty("email")]
    public string Email { get; set; }
    [JsonProperty("avatarUri")]
    public string AvatarUri { get; set; }
    [JsonProperty("lastSignInTimestamp")]
    public DateTime? LastSignInTimestamp { get; set; }
    [JsonProperty("createdTimestamp")]
    public DateTime? CreatedTimestamp { get; set; }
    [JsonProperty("favoriteMovies")]
    public List<int> FavoriteMovies { get; set; } = new List<int>();
}