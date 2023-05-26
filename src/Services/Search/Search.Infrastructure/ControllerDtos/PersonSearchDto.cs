namespace Search.Infrastructure.ControllerDtos;

public class PersonSearchDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string KnownFor { get; set; } = default!;
    public string ProfilePath { get; set; } = default!;
}