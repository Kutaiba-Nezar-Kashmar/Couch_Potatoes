namespace User.Domain;

public class CouchPotatoUser
{
    public string Id { get; set; }
    public string DisplayName { get; set; }
    public string Email { get; set; }
    public string AvatarUri { get; set; }
    public DateTime? LastSignInTimestamp { get; set; }
    public DateTime? CreatedTimestamp { get; set; }
    public List<int> FavoriteMovies { get; set; } = new List<int>();
}