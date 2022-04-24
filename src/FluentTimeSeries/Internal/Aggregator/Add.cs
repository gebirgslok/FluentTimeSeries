using System;

namespace FluentTimeSeries.Internal.Aggregator;

internal class Add : ITimeSeriesAggregator
{
    public double Aggregate(double prevValue, IFt currentFt, TimeSpan t)
    {
        return prevValue + currentFt.Apply(t);
    }
}