namespace MovieInformation.Domain.Models;

public class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
    public string Gender { get; set; }
    public string ImageUri { get; set; }
    public string KnownFor { get; set; }
    public string OriginalName { get; set; }
    public float Popularity { get; set; }
    public string Bio { get; set; }
    public DateTime DeathDate { get; set; }
}