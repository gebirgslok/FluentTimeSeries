using System;

namespace FluentTimeSeries.Internal.Transformer;

internal class Pow : ITimeSeriesTransformer
{
    private readonly double _exponent;

    public Pow(double exponent)
    {
        _exponent = exponent;
    }

    public double Transform(double prevValue)
    {
        return Math.Pow(prevValue, _exponent);
    }
}