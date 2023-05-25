namespace MovieInformation.Infrastructure.ControllerDtos.Person;

public class CastMemberDto
{
    public bool IsAdult { get; set; }
    public int Gender { get; set; }
    public int Id { get; set; }
    public string KnownForDepartment { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string OriginalName { get; set; } = default!;
    public float Popularity { get; set; }
    public string ProfilePath { get; set; } = default!;
    public int CastId { get; set; }
    public string Character { get; set; } = default!;
    public string CreditId { get; set; } = default!;
    public int Order { get; set; }
}