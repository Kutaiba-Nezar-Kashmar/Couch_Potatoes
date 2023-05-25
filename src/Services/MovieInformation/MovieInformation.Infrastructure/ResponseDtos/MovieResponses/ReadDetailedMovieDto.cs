namespace MovieInformation.Infrastructure.ResponseDtos.MovieResponses;

public class ReadKeywordDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
}

public class ReadLanguageDto
{
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
}

public class ReadGenreDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
}