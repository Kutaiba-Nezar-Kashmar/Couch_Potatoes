namespace Search.Infrastructure.Util;

public static class DateTimeParser
{
    public static DateTime? ParseDateTime(string dateTimeAsString)
    {
        if (string.IsNullOrEmpty(dateTimeAsString) ||
            string.IsNullOrWhiteSpace(dateTimeAsString)) return null;

        if (DateTime.TryParse(dateTimeAsString, out var dateTime))
            return dateTime;

        return null;
    }
}