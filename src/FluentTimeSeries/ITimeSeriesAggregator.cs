using System;

namespace FluentTimeSeries;

public interface ITimeSeriesAggregator
{
    double Aggregate(double prevValue, IFt currentFt, TimeSpan t);
}