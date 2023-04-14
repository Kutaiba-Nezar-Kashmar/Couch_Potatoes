namespace MovieInformation.Infrastructure.TmbdDto.MovieDto;
public class MovieDetailGenre
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class MovieDetailProductionCompany
{
    public int Id { get; set; }
    public string LogoPath { get; set; }
    public string Name { get; set; }
    public string OriginCountry { get; set; }
}

public class MovieDetailProductionCountry
{
    public string Iso31661 { get; set; }
    public string Name { get; set; }
}

public class MovieDetail
{
    public bool Adult { get; set; }
    public string BackdropPath { get; set; }
    public object BelongsToCollection { get; set; }
    public int Budget { get; set; }
    public List<MovieDetailGenre> Genres { get; set; }
    public string Homepage { get; set; }
    public int Id { get; set; }
    public string ImdbId { get; set; }
    public string OriginalLanguage { get; set; }
    public string OriginalTitle { get; set; }
    public string Overview { get; set; }
    public double Popularity { get; set; }
    public object PosterPath { get; set; }
    public List<MovieDetailProductionCompany> ProductionCompanies { get; set; }
    public List<MovieDetailProductionCountry> ProductionCountries { get; set; }
    public string ReleaseDate { get; set; }
    public int Revenue { get; set; }
    public int Runtime { get; set; }
    public List<MovieDetailSpokenLanguage> SpokenLanguages { get; set; }
    public string Status { get; set; }
    public string Tagline { get; set; }
    public string Title { get; set; }
    public bool Video { get; set; }
    public double VoteAverage { get; set; }
    public int VoteCount { get; set; }
}

public class MovieDetailSpokenLanguage
{
    public string Iso6391 { get; set; }
    public string Name { get; set; }
}

