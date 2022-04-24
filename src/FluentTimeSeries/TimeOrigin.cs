using System;

namespace FluentTimeSeries;

public class TimeOrigin
{
    private readonly DateTime _dateTime;

    public static TimeOrigin Now => new(DateTime.Now);

    public static TimeOrigin UtcNow => new(DateTime.UtcNow);

    public static TimeOrigin TimeToday(int hour, int minute = 0, int second = 0, int millisecond = 0)
    {
        var today = DateTime.Today;
        var year = today.Year;
        var month = today.Month;
        var day = today.Day;
        var dateTime = new DateTime(year, month, day, hour, minute, second, millisecond);
        return new TimeOrigin(dateTime);
    }

    private TimeOrigin(DateTime dateTime)
    {
        _dateTime = dateTime;
    }

    internal TimeSpan ToRelativeTimestamp(DateTime absoluteTimestamp)
    {
        return absoluteTimestamp - _dateTime;
    }

    internal DateTime Add(TimeSpan value) => _dateTime.Add(value);
}