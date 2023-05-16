namespace Metrics.Application.PersonMetrics.Exceptions;

public class FetchPersonMoveCreditsException : Exception
{
    public FetchPersonMoveCreditsException(string message, Exception innerException) 
        : base(message, innerException)
    {
    }
}