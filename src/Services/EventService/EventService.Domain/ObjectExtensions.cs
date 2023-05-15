namespace EventService.Domain;

public static class ObjectExtensions
{
    public static T Cast<T>(this object elm)
    {
        return (T) elm;
    }
}