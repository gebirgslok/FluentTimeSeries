using System;

namespace FluentTimeSeries.Internal.Fn;

internal class Sine : IFt
{
    private readonly PeriodicFunctionParams _p;

    public Sine(PeriodicFunctionParams p)
    {
        _p = p;
    }

    public double Apply(TimeSpan t)
    {
        var a = _p.Amplitude;
        var b = 2 * Math.PI / _p.Period.TotalMilliseconds;
        var c = _p.PhaseShift.TotalMilliseconds;
        var d = _p.VerticalShift;
        var tt  = t.TotalMilliseconds;
        return a * Math.Sin(b * (tt + c)) + d;
    }
}