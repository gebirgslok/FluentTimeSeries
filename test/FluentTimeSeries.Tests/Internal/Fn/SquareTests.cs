using System;
using FluentAssertions;
using FluentTimeSeries.Internal.Fn;
using Xunit;

namespace FluentTimeSeries.Tests.Internal.Fn;

public class SquareTests
{
    private const double _epsT = 0.1;

    [Theory]
    [InlineData(10, 1337.0)]
    [InlineData(42, -15.42)]
    public void Apply_With_Known_Points_No_PhaseShift(int periodSeconds, double amplitude)
    {
        var p = new PeriodicFunctionParams
        {
            Amplitude = amplitude,
            Period = TimeSpan.FromSeconds(periodSeconds)
        };

        var square = new Square(p);

        //t=0
        square
            .Apply(new TimeSpan(0))
            .Should()
            .BeApproximately(amplitude, TestDefaults.ComparisonEpsilon);

        //t=p/4
        square
            .Apply(TimeSpan.FromSeconds(0.25 * periodSeconds))
            .Should()
            .BeApproximately(amplitude, TestDefaults.ComparisonEpsilon);

        //t=p/2 (-eps)
        square
            .Apply(TimeSpan.FromSeconds(0.5 * periodSeconds - _epsT))
            .Should()
            .BeApproximately(amplitude, TestDefaults.ComparisonEpsilon);

        //t=p/2 (+eps)
        square
            .Apply(TimeSpan.FromSeconds(0.5 * periodSeconds + _epsT))
            .Should()
            .BeApproximately(-amplitude, TestDefaults.ComparisonEpsilon);

        //t=p3/4
        square
            .Apply(TimeSpan.FromSeconds(0.75 * periodSeconds))
            .Should()
            .BeApproximately(-amplitude, TestDefaults.ComparisonEpsilon);
    }
}