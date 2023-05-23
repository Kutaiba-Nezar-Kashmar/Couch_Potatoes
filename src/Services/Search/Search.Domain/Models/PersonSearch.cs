namespace Search.Domain.Models;

public class PersonSearch
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string KnownFor { get; set; } = default!;
    public string ProfilePath { get; set; } = default!;
}