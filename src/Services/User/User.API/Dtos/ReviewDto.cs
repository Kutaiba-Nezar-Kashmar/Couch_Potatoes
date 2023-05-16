namespace User.API.Dtos;

public class ReviewDto
{
    public Guid UserId { get; set; }
    public int MovieId { get; set; }
    public float Rating { get; set; }
    public string ReviewText { get; set; }
    public DateTime CreationDate { get; set; }
    public IReadOnlyCollection<VoteDto> Votes { get; set; } = new List<VoteDto>();

    public bool IsValid()
    {
        return MovieId > 0 && Rating is >= 0 and <= 10 && UserId != null && CreationDate <= DateTime.UtcNow;
    }
}