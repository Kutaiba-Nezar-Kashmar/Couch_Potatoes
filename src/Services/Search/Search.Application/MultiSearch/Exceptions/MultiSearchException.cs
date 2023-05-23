namespace Search.Application.MultiSearch.Exceptions;

public class MultiSearchException : Exception
{
    public MultiSearchException(string message, Exception innerException) :
        base(message, innerException)
    {
    }
}