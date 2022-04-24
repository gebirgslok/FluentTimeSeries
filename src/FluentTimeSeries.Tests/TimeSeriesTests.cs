using System;
using FluentAssertions;
using FluentTimeSeries.Internal;
using FluentTimeSeries.Internal.Fn;
using Xunit;

namespace FluentTimeSeries.Tests;

public class TimeSeriesTests
{
    [Fact]
    public void Sample_With_Just_One_Sine()
    {
        var origin = TimeOrigin.Now;
        var period = TimeSpan.FromSeconds(1);

        var ft = new Sine(new PeriodicFunctionParams
        {
            Period = period
        });
        var element = new FtProxyComponent(ft);

        var timeSeries = new TimeSeries(new ITimeSeriesComponent[]{ element }, origin);

        var timestamp = origin.Add(period);

        var dataPoint = timeSeries.Sample(timestamp);

        var value = dataPoint.Value;

        //Sine intersects y axis after 1 period and therefore must be ~0.
        value.Should().BeApproximately(0.0, 1e-10);
    }
}
