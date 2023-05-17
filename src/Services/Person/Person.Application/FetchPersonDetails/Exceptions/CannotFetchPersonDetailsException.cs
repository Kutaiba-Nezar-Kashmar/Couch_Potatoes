namespace Person.Application.FetchPersonDetails.Exceptions;

public class CannotFetchPersonDetailsException : Exception
{
    public CannotFetchPersonDetailsException(string message,
        Exception innerException)
    {
    }
}