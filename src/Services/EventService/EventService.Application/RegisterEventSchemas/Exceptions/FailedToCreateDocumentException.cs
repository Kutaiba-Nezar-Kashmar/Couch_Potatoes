namespace EventService.Application.RegisterEventSchemas.Exceptions;

public class FailedToCreateDocumentException: Exception
{
    public FailedToCreateDocumentException():base()
    {
        
    }

    public FailedToCreateDocumentException(string msg): base(msg)
    {
        
    }
}