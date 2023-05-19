namespace User.API.Dtos;

public class VoteDto
{
    public string Direction { get; set; }
    public Guid Id { get; set; }
    public string UserId { get; set; }
}