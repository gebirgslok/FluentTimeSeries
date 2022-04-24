using System;

namespace FluentTimeSeries.Internal.Fn;

internal class GaussianRandom : IFt
{
    private readonly GaussianRandomConfiguration _p;

    public GaussianRandom(GaussianRandomConfiguration p)
    {
        _p = p;
    }

    public double Apply(TimeSpan t)
    {
        //Code taken from https://stackoverflow.com/questions/218060
        var u1 = 1.0 - Rng.NextDouble();
        var u2 = 1.0 - Rng.NextDouble();

        var randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                            Math.Sin(2.0 * Math.PI * u2);
        var randNormal =
            _p.Mean + _p.StdDev * randStdNormal;

        return randNormal;
    }
}