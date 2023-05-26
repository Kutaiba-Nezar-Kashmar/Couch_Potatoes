namespace MovieInformation.Domain.Models.ProductionCompanie;

public class MovieProductionCompany
{
    public int Id { get; set; }
    public string LogoPath { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string OriginCountry { get; set; } = default!;
}