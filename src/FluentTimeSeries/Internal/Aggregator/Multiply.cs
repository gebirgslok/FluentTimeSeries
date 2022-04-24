using System;

namespace FluentTimeSeries.Internal.Aggregator;

internal class Multiply : ITimeSeriesAggregator
{
    public double Aggregate(double prevValue, IFt currentFt, TimeSpan t)
    {
        return prevValue * currentFt.Apply(t);
    }
}