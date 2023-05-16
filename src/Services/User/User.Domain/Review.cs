namespace User.Domain;

public class Review
{
    public Guid UserId { get; set; }
    public int MovieId { get; set; }
    public float Rating { get; set; }
    public string ReviewText { get; set; }
    public DateTime CreationDate { get; set; }
    public IReadOnlyCollection<Vote> Votes { get; set; } = new List<Vote>();

    public bool IsValid()
    {
        return MovieId > 0 && Rating is >= 0 and <= 10 && UserId != null && CreationDate <= DateTime.UtcNow;
    }
}