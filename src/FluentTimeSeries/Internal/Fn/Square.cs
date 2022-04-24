using System;

namespace FluentTimeSeries.Internal.Fn;

internal class Square : IFt
{
    private readonly PeriodicFunctionParams _p;

    public Square(PeriodicFunctionParams p)
    {
        _p = p;
    }

    public double Apply(TimeSpan t)
    {
        var tt = t.TotalMilliseconds + _p.PhaseShift.TotalMilliseconds;
        var temp = tt / _p.Period.TotalMilliseconds;
        var y = (2 * Math.Floor(temp) - Math.Floor(2 * temp));
        y *= 2;
        y += 1;
        y *= _p.Amplitude; //[-a, a]
        y += _p.VerticalShift; //[vs, a + vs]

        return y;
    }
}