namespace Search.Application.MultiSearch.Exceptions;

public class CannotMultiSearchException : Exception
{
    public CannotMultiSearchException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}