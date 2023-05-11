namespace MovieInformation.Infrastructure.Util;

public static class ObjectExtensions
{
    public static T Cast<T>(this object elm)
    {
        return (T) elm;
    }
}