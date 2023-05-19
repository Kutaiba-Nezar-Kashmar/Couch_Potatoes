using Newtonsoft.Json;

namespace User.API.Dtos;

public class AddMovieToFavoriteDto
{
    [JsonProperty("movieId")]
    public int MovieId{ get; set; }
}