using System;

namespace FluentTimeSeries.Internal.Fn;

internal class Sawtooth : IFt
{
    private readonly PeriodicFunctionParams _p;

    public Sawtooth(PeriodicFunctionParams p)
    {
        _p = p;
    }

    public double Apply(TimeSpan t)
    {
        var tt = t.TotalMilliseconds + _p.PhaseShift.TotalMilliseconds;
        var temp = tt / _p.Period.TotalMilliseconds;
        var y = temp - Math.Floor(0.5 + temp); //[-.5, .5]
        y *= 2; //[-1, 1]
        y *= _p.Amplitude; //[-a, a]
        y += _p.VerticalShift; //[vs, a + vs]

        return y;
    }
}