using System;
using System.Collections.Generic;
using System.Linq;
using FluentTimeSeries.Internal;

namespace FluentTimeSeries;

public class TimeSeries
{
    internal ITimeSeriesComponent[] Components { get; }

    public TimeOrigin TimeOrigin { get; }

    internal TimeSeries(IEnumerable<ITimeSeriesComponent> components, 
        TimeOrigin? timeOrigin = null)
    {
        TimeOrigin = timeOrigin ?? TimeOrigin.Now;
        Components = components.ToArray();
    }

    private DataPoint CalculateDataPoint(TimeSpan relativeTimestamp, DateTime absoluteTimestamp)
    {
        var context = new CurrentSampleContext(relativeTimestamp, absoluteTimestamp);

        foreach (var timeSeriesElement in Components)
        {
            timeSeriesElement.Next(context);
        }

        return context.ToDataPoint();
    }

    public DataPoint[] Block(int numOfPoints, double samplingIntervalSeconds, DateTime? startTimestamp = null)
    {
        var dataPoints = new DataPoint[numOfPoints];

        var absoluteTimestamp = startTimestamp ?? TimeOrigin.DateTime;
        var relativeTimestamp = TimeOrigin.ToOriginOffset(absoluteTimestamp); 

        for (var i = 0; i < numOfPoints; i++)
        {
            var point = CalculateDataPoint(relativeTimestamp, absoluteTimestamp);
            dataPoints[i] = point;

            if (i >= numOfPoints - 1)
            {
                continue;
            }

            absoluteTimestamp = absoluteTimestamp.AddSeconds(samplingIntervalSeconds);
            relativeTimestamp = TimeOrigin.ToOriginOffset(absoluteTimestamp);
        }

        return dataPoints;
    }

    public DataPoint[] Block(TimeSpan length, double samplingIntervalSeconds, DateTime? startTimestamp = null)
    {
        throw new NotImplementedException();
    }

    public DataPoint Sample(DateTime? timestamp = null)
    {
        var timestampOrNow = timestamp ?? TimeOrigin.GetNow();
        var relativeTimestamp = TimeOrigin.ToOriginOffset(timestampOrNow);
        return CalculateDataPoint(relativeTimestamp, timestampOrNow);
    }
}