namespace Metrics.Application.PersonMetrics.Exceptions;

public class StatisticsException : Exception
{
    public StatisticsException(string message, Exception innerException) :
        base(message, innerException)
    {
    }
}