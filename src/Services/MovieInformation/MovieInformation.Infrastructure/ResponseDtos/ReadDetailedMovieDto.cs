using MovieInformation.Domain.Models;
using MovieInformation.Infrastructure.TmbdDto.MovieDto;

namespace MovieInformation.Infrastructure.ResponseDtos;

public class ReadDetailedMovieDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Summary { get; set; }
    public string ImageUri { get; set; }
    public string BackdropUri { get; set; }
    public float TmdbScore { get; set; }
    public string TagLine { get; set; }
    public int TmdbVoteCount { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int RunTime { get; set; }
    public bool IsForKids { get; set; }

    public string
        Status
    {
        get;
        set;
    } // TODO: make into Enum and find out how to deal with white spaces

    public string Homepage { get; set; }
    public string TrailerUri { get; set; }
    public long Budget { get; set; }
    public long Revenue { get; set; }
    public IEnumerable<ReadKeywordDto> Keywords { get; set; }
    public IEnumerable<ReadLanguageDto> Languages { get; set; }
    public IEnumerable<ReadGenreDto> Genres { get; set; }
    public IEnumerable<PosterResponseDto> Posters { get; set; }
    public IEnumerable<BackdropImageResponseDto> Backdrops { get; set; }
    public IEnumerable<LogoResponseDto> Logos { get; set; }
}

public class ReadKeywordDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class ReadLanguageDto
{
    public string Code { get; set; }
    public string Name { get; set; }
}

public class ReadGenreDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}