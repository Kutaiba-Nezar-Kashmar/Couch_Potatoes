namespace EventService.Application.RegisterEventSchemas.Validation;

public class Guard
{
    public Guard AgainstNull(object obj, string? msg = null)
    {
        if (obj is null)
        {
            throw new ArgumentNullException(msg ?? "Object cannot be null");
        }

        return this;
    }

    public Guard AgainstNullOrEmptyString(string s, string? msg = null)
    {
        if (string.IsNullOrWhiteSpace(s) || string.IsNullOrEmpty(s))
        {
            throw new ArgumentException(msg ?? "String cannot be empty");
        }

        return this;
    }
}