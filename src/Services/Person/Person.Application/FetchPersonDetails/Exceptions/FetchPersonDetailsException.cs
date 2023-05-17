namespace Person.Application.FetchPersonDetails.Exceptions;

public class FetchPersonDetailsException : Exception
{
    public FetchPersonDetailsException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}