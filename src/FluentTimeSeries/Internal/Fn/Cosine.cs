using System;

namespace FluentTimeSeries.Internal.Fn;

internal class Cosine : IFt
{
    private readonly PeriodicFunctionParams _p;

    public Cosine(PeriodicFunctionParams p)
    {
        _p = p;
    }

    public double Apply(TimeSpan t)
    {
        var a = _p.Amplitude;
        var b = 2 * Math.PI / _p.Period.TotalMilliseconds;
        var c = _p.PhaseShift.TotalMilliseconds;
        var d = _p.VerticalShift;
        var tt = t.TotalMilliseconds;
        return a * Math.Cos(b * (tt + c)) + d;
    }
}