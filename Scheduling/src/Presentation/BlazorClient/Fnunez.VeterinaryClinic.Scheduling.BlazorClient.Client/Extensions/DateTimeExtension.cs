namespace System;

public static class DateTimeExtension
{
    public static DateTime ToUnspecifiedKind(this DateTime dateTime)
    {
        return DateTime.SpecifyKind(dateTime, DateTimeKind.Unspecified);
    }
}