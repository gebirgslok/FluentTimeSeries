using System;

namespace FluentTimeSeries.Internal.Fn;

internal class Triangle : IFt
{
    private readonly PeriodicFunctionParams _p;

    public Triangle(PeriodicFunctionParams p)
    {
        _p = p;
    }

    public double Apply(TimeSpan t)
    {
        var tt = t.TotalMilliseconds + _p.PhaseShift.TotalMilliseconds;
        var temp = tt / _p.Period.TotalMilliseconds;
        var y = Math.Abs(2 * (temp - Math.Floor(temp + 0.5)));
        y *= 2;
        y -= 1;
        y *= _p.Amplitude; //[-a, a]
        y += _p.VerticalShift; //[vs, a + vs]
        return y;
    }
}