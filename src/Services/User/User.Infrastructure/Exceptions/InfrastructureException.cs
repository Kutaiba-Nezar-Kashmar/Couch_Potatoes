namespace User.Infrastructure.Exceptions;

public class InfrastructureException: Exception
{
    public InfrastructureException(string? reason = null, Exception? inner = null): base(reason, inner)
    {
        
    }
}