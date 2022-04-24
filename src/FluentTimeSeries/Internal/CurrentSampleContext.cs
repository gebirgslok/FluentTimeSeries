using System;

namespace FluentTimeSeries.Internal;

internal sealed class CurrentSampleContext : ICurrentSampleContext
{
    public double CurrentValue { get; private set; }

    public TimeSpan RelativeTimestamp { get; }

    public DateTime Timestamp { get; }

    public CurrentSampleContext(TimeSpan relativeTimestamp, 
        DateTime timestamp, 
        double initialValue = 0.0)
    {
        CurrentValue = initialValue;
        RelativeTimestamp = relativeTimestamp;
        Timestamp = timestamp;
    }

    public void SetNextValue(double value)
    {
        CurrentValue = value;
    }

    public DataPoint ToDataPoint() => new(CurrentValue, Timestamp, RelativeTimestamp);
}