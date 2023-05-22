namespace MovieInformation.Infrastructure.Exceptions;

public class MappingException: Exception
{
    public MappingException(string? message = null, Exception? inner = null): base(message, inner)
    {
        
    }
}