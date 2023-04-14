namespace MovieInformation.Infrastructure.TmbdDto.PersonDto;

public class PersonDetails
{
    public string Birthday { get; set; }
    public string KnownForDepartment { get; set; }
    public object Deathday { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }
    public List<string> AlsoKnownAs { get; set; }
    public int Gender { get; set; }
    public string Biography { get; set; }
    public double Popularity { get; set; }
    public string PlaceOfBirth { get; set; }
    public string ProfilePath { get; set; }
    public bool Adult { get; set; }
    public string ImdbId { get; set; }
    public object Homepage { get; set; }
}