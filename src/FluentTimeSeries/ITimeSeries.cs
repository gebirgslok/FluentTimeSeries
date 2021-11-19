using System;

namespace FluentTimeSeries
{
    public interface ITimeSeries
    {
        DataPoint Next(DateTime? timestamp = null);

        DataPoint[] Block(int numOfPoints, double samplingInterval, DateTime? origin);

        DataPoint[] Block(TimeSpan length, double samplingInterval, DateTime? origin);
    }
}