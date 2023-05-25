namespace MovieInformation.Infrastructure.ControllerDtos.Images;

public class MovieImageDto
{
    public float Height { get; set; }
    public string FilePath { get; set; } = default!;
    public int Width { get; set; }
}