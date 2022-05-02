using System;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace FluentTimeSeries.Tests;

public class TimeSeriesTests
{
    [Fact]
    public void Block_Length_With_Cosine_Two_Periods()
    {
        var origin = TimeOrigin.Now;
        var period = TimeSpan.FromSeconds(100);

        var series = TimeSeriesBuilder
            .New().Cosine(period: period)
            .SetTimeOrigin(origin)
            .Build();

        var dataPoints = series.Block(TimeSpan.FromSeconds(200.15), 0.1); //2001 points

        dataPoints[0].Value.Should().BeApproximately(1.0, TestDefaults.ComparisonEpsilon);
        dataPoints[250].Value.Should().BeApproximately(0.0, TestDefaults.ComparisonEpsilon);
        dataPoints[500].Value.Should().BeApproximately(-1.0, TestDefaults.ComparisonEpsilon);
        dataPoints[750].Value.Should().BeApproximately(0.0, TestDefaults.ComparisonEpsilon);
        dataPoints[1000].Value.Should().BeApproximately(1.0, TestDefaults.ComparisonEpsilon);
        dataPoints[1250].Value.Should().BeApproximately(0.0, TestDefaults.ComparisonEpsilon);
        dataPoints[1500].Value.Should().BeApproximately(-1.0, TestDefaults.ComparisonEpsilon);
        dataPoints[1750].Value.Should().BeApproximately(0.0, TestDefaults.ComparisonEpsilon);
        dataPoints[2000].Value.Should().BeApproximately(1.0, TestDefaults.ComparisonEpsilon);
    }

    [Fact]
    public void Block_NumOfPoints_With_Sine_One_Period()
    {
        var origin = TimeOrigin.Now;
        var period = TimeSpan.FromSeconds(100);

        var series = TimeSeriesBuilder
            .New().Sine(period: period)
            .SetTimeOrigin(origin)
            .Build();

        var dataPoints = series.Block(1001, 0.1);

        dataPoints[0].Value.Should().BeApproximately(0.0, TestDefaults.ComparisonEpsilon);
        dataPoints[250].Value.Should().BeApproximately(1.0, TestDefaults.ComparisonEpsilon);
        dataPoints[500].Value.Should().BeApproximately(0.0, TestDefaults.ComparisonEpsilon);
        dataPoints[750].Value.Should().BeApproximately(-1.0, TestDefaults.ComparisonEpsilon);
        dataPoints.Last().Value.Should().BeApproximately(0.0, TestDefaults.ComparisonEpsilon);
    }

    [Fact]
    public void Sample_With_Just_One_Sine()
    {
        var origin = TimeOrigin.Now;
        var period = TimeSpan.FromSeconds(1);

        var series = TimeSeriesBuilder
            .New().Sine(period: period)
            .SetTimeOrigin(origin)
            .Build();

        var timestamp = origin.Add(period);


        var dataPoint = series.Sample(timestamp);

        var value = dataPoint.Value;

        //Sine intersects y axis after 1 period and therefore must be ~0.
        value.Should().BeApproximately(0.0, 1e-10);
    }
}
