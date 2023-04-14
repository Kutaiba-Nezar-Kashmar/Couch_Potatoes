namespace MovieInformation.Domain.Models;

public class Cast : MoviePerson
{
    public int CastId { get; set; }
    public string Character { get; set; }
    public int Order { get; set; }
}