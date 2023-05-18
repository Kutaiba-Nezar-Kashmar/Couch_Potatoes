namespace Person.Infrastructure.Exceptions;

public class HttpException : Exception
{
    public HttpException(string message) : base(message)
    {
    }
}