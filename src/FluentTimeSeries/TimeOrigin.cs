using System;

namespace FluentTimeSeries;

public class TimeOrigin
{
    private readonly bool _isUtc;

    public DateTime DateTime { get; }

    public static TimeOrigin Now => new(DateTime.Now, false);

    public static TimeOrigin UtcNow => new(DateTime.UtcNow, true);

    public static TimeOrigin TimeToday(int hour, int minute = 0, int second = 0, int millisecond = 0)
    {
        var today = DateTime.Today;
        var year = today.Year;
        var month = today.Month;
        var day = today.Day;
        var dateTime = new DateTime(year, month, day, hour, minute, second, millisecond);
        return new TimeOrigin(dateTime, false);
    }

    private TimeOrigin(DateTime dateTime, bool isUtc)
    {
        DateTime = dateTime;
        _isUtc = isUtc;
    }

    internal TimeSpan ToOriginOffset(DateTime absoluteTimestamp)
    {
        return absoluteTimestamp - DateTime;
    }

    internal DateTime Add(TimeSpan value) => DateTime.Add(value);

    internal DateTime GetNow() => _isUtc ? DateTime.UtcNow : DateTime.Now;
}