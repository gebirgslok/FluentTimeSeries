using System;

namespace FluentTimeSeries.Internal.Transformer;

internal class Abs : ITimeSeriesTransformer
{
    public double Transform(double prevValue)
    {
        return Math.Abs(prevValue);
    }
}
