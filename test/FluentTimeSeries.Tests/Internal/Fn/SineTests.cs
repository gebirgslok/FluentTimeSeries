using System;
using FluentAssertions;
using FluentTimeSeries.Internal;
using FluentTimeSeries.Internal.Fn;
using Xunit;

namespace FluentTimeSeries.Tests.Internal.Fn;

public class SineTests
{
    [Theory]
    [InlineData(0, 0.0)]
    [InlineData(250, -1.0)]
    [InlineData(500, 0.0)]
    [InlineData(750, 1.0)]
    [InlineData(1000, 0.0)]
    public void Apply_With_Known_Points_With_PhaseShift_And_A(int tInMs, double expected)
    {
        //Use some random amplitudes
        var amp = Rng.Next(1.5, 42.0);

        //Period is 1s
        var sine = new Sine(new PeriodicFunctionParams
        { 
            PhaseShift = TimeSpan.FromMilliseconds(500), 
            Amplitude = amp
        });

        var ft = sine.Apply(TimeSpan.FromMilliseconds(tInMs));

        ft.Should().BeApproximately(amp * expected, 1e-10);
    }

    [Theory]
    [InlineData(0, 0.0)]
    [InlineData(250, 1.0)]
    [InlineData(500, 0.0)]
    [InlineData(750, -1.0)]
    [InlineData(1000, 0.0)]
    public void Apply_With_Known_Points_With_Default_Params(int tInMs, double expected)
    {
        //Period is 1s
        var sine = new Sine(new PeriodicFunctionParams());

        var ft = sine.Apply(TimeSpan.FromMilliseconds(tInMs));

        ft.Should().BeApproximately(expected, 1e-15);
    }
}
