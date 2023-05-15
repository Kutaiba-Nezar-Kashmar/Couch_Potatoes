namespace EventService.Domain.Exceptions;

public class UnknownPropertyTypeException: Exception
{
    public UnknownPropertyTypeException(): base()
    {
        
    }

    public UnknownPropertyTypeException(string msg): base(msg)
    {
        
    }
}