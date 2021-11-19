using System;

namespace FluentTimeSeries
{
    public readonly struct DataPoint
    {
        public double Value { get; }

        public DateTime Timestamp { get; }

        public TimeSpan OffsetStart { get; }

        public TimeSpan OffsetPrev { get; }

        public DataPoint(double value, DateTime timestamp, TimeSpan startOffset, TimeSpan prevOffset)
        {
            Value = value;
            Timestamp = timestamp;
            OffsetStart = startOffset;
            OffsetPrev = prevOffset;
        }

        public override string ToString()
        {
            return $"[+{OffsetStart.TotalSeconds:F3}s] {Value:F3}";
        }
    }
}