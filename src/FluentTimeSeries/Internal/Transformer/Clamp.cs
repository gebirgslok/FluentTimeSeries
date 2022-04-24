using System;

namespace FluentTimeSeries.Internal.Transformer;

internal class Clamp : ITimeSeriesTransformer
{
    private readonly ClampConfiguration _p;

    public Clamp(ClampConfiguration p)
    {
        _p = p;
    }

    public double Transform(double prevValue)
    {
        var max = _p.Max;
        var min = _p.Min;
        var next = prevValue;

        if (max.HasValue)
        {
            next = Math.Min(next, max.Value);
        }

        if (min.HasValue)
        {
            next = Math.Max(next, min.Value);
        }

        return next;
    }
}