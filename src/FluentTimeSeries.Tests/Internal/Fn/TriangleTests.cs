using System;
using FluentAssertions;
using FluentTimeSeries.Internal.Fn;
using Xunit;

namespace FluentTimeSeries.Tests.Internal.Fn;

public class TriangleTests
{
    [Theory]
    [InlineData(10, 42.1337)]
    [InlineData(80, 1.0)]
    [InlineData(42, -1337.0)]
    public void Apply_Known_Points_No_PhaseShift(int periodSeconds, double amplitude)
    {
        var period = TimeSpan.FromSeconds(periodSeconds);
        var p = new PeriodicFunctionParams
        {
            Amplitude = amplitude,
            Period = period
        };

        var triangle = new Triangle(p);

        //t=0
        triangle
            .Apply(new TimeSpan(0))
            .Should()
            .BeApproximately(-amplitude, TestDefaults.ComparisonEpsilon);

        //t=p/2
        triangle
            .Apply(TimeSpan.FromSeconds(0.5 * periodSeconds))
            .Should()
            .BeApproximately(amplitude, TestDefaults.ComparisonEpsilon);

        //t=p
        triangle
            .Apply(TimeSpan.FromSeconds(periodSeconds))
            .Should()
            .BeApproximately(-amplitude, TestDefaults.ComparisonEpsilon);

        //t=100*p
        triangle
            .Apply(TimeSpan.FromSeconds(100.0 * periodSeconds))
            .Should()
            .BeApproximately(-amplitude, TestDefaults.ComparisonEpsilon);
    }
}