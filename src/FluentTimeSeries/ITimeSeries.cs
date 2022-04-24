using System;

namespace FluentTimeSeries;

public class TimeSeriesOrigin
{
    public DateTime Origin => DateTime.Now;

    public TimeSeriesOrigin()
    {
        
    }
}

public interface ITimeSeries
{
    DataPoint[] Block(int numOfPoints, double samplingInterval, DateTime? origin);

    DataPoint[] Block(TimeSpan length, double samplingInterval, DateTime? origin);

    DataPoint Sample(DateTime? timestamp = null);
}