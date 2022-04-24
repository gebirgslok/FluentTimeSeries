using System;

namespace FluentTimeSeries;

public readonly struct DataPoint
{
    public double Value { get; }

    public DateTime Timestamp { get; }

    public TimeSpan OriginOffset { get; }

    internal DataPoint(double value, DateTime timestamp, TimeSpan originOffset)
    {
        Value = value;
        Timestamp = timestamp;
        OriginOffset = originOffset;
    }

    public override string ToString()
    {
        return $"[+{OriginOffset.TotalSeconds:F3}s] {Value:F3}";
    }
}