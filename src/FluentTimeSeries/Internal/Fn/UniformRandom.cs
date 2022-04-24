using System;

namespace FluentTimeSeries.Internal.Fn;

internal class UniformRandom : IFt
{
    private readonly UniformRandomConfiguration _p;

    public UniformRandom(UniformRandomConfiguration p)
    {
        _p = p;
    }

    public double Apply(TimeSpan t)
    {
        return Rng.Next(_p.Min, _p.Max);
    }
}