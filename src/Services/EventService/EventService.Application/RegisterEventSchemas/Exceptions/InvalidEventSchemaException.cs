namespace EventService.Application.RegisterEventSchemas.Exceptions;

public class InvalidEventSchemaException: Exception
{
    public InvalidEventSchemaException(): base()
    {
        
    }

    public InvalidEventSchemaException(string msg): base(msg)
    {
        
    }
}