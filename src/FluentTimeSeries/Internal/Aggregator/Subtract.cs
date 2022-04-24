using System;

namespace FluentTimeSeries.Internal.Aggregator;

internal class Subtract : ITimeSeriesAggregator
{
    public double Aggregate(double prevValue, IFt currentFt, TimeSpan t)
    {
        return prevValue - currentFt.Apply(t);
    }
}