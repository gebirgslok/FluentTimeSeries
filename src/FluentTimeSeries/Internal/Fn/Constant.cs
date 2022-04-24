using System;

namespace FluentTimeSeries.Internal.Fn;

internal class Constant : IFt
{
    private readonly double _constantValue;

    public Constant(double constantValue)
    {
        _constantValue = constantValue;
    }

    public double Apply(TimeSpan t)
    {
        return _constantValue;
    }
}