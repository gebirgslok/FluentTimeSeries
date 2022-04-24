using System;
using System.Collections.Generic;
using System.Linq;
using FluentTimeSeries.Internal;

namespace FluentTimeSeries;

public class TimeSeries
{
    private readonly ITimeSeriesComponent[] _elements;

    public TimeOrigin TimeOrigin { get; }

    internal TimeSeries(IEnumerable<ITimeSeriesComponent> elements, 
        TimeOrigin? timeOrigin = null)
    {
        TimeOrigin = timeOrigin ?? TimeOrigin.UtcNow;
        _elements = elements.ToArray();
    }

    public DataPoint[] Block(int numOfPoints, double samplingInterval, DateTime? origin)
    {
        throw new NotImplementedException();
    }

    public DataPoint[] Block(TimeSpan length, double samplingInterval, DateTime? origin)
    {
        throw new NotImplementedException();
    }

    public DataPoint Sample(DateTime? timestamp = null)
    {
        var timestampOrNow = timestamp ?? DateTime.Now;
        var relativeTimestamp = TimeOrigin.ToRelativeTimestamp(timestampOrNow);
        var context = new CurrentSampleContext(relativeTimestamp, timestampOrNow);

        foreach (var timeSeriesElement in _elements)
        {
            timeSeriesElement.Next(context);
        }

        return context.ToDataPoint();
    }
}