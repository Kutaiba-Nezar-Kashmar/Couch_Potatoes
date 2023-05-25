namespace MovieInformation.Infrastructure.Exceptions;

public class DeserializeException : Exception
{
    public DeserializeException() : base()
    {
    }

    public DeserializeException(string msg) : base(msg)
    {
    }
}