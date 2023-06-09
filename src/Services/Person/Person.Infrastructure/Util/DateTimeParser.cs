﻿namespace Person.Infrastructure.Util;

public static class DateTimeParser
{
    public static DateTime? ParseDateTime(string dateTimeAsString)
    {
        if (string.IsNullOrEmpty(dateTimeAsString) ||
            string.IsNullOrWhiteSpace(dateTimeAsString)) return null;

        DateTime dateTime;
        if (DateTime.TryParse(dateTimeAsString, out dateTime)) return dateTime;

        return null;
    }
}