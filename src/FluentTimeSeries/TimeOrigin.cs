using System;

namespace FluentTimeSeries;

public class TimeOrigin
{
    public DateTime DateTime { get; }

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
        DateTime = dateTime;
    }

    internal TimeSpan ToOriginOffset(DateTime absoluteTimestamp)
    {
        return absoluteTimestamp - DateTime;
    }

    internal DateTime Add(TimeSpan value) => DateTime.Add(value);
}