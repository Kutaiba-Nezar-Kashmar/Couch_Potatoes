namespace MovieInformation.Infrastructure.TmbdDto.CreditsDto;

public class Media
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string OriginalName { get; set; }
    public string Character { get; set; }
    public List<object> Episodes { get; set; }
    public List<Season> Seasons { get; set; }
}

public class Person
{
    public string Name { get; set; }
    public int Id { get; set; }
}

public class Season
{
    public string AirDate { get; set; }
    public string PosterPath { get; set; }
    public int SeasonNumber { get; set; }
}

public class CreditsDetail
{
    public string CreditType { get; set; }
    public string Department { get; set; }
    public string Job { get; set; }
    public Media Media { get; set; }
    public string MediaType { get; set; }
    public string Id { get; set; }
    public Person Person { get; set; }
}