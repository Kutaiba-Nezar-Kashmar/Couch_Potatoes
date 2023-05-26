namespace Search.Domain.Models;

public class PersonSearch
{
    public bool IsAdult { get; set; }
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string OriginalName { get; set; } = default!;
    public string MediaType { get; set; } = default!;
    public float Popularity { get; set; }
    public int Gender { get; set; }
    public string KnownForDepartment { get; set; } = default!;
    public string ProfilePath { get; set; } = default!;
    public IReadOnlyCollection<object> KnownFor { get; set; } = default!;
}