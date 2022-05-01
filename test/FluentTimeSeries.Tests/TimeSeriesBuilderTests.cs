using System;
using FluentAssertions;
using Xunit;

namespace FluentTimeSeries.Tests;

public class TimeSeriesBuilderTests
{
    [Fact]
    public void Build_UniformRandom_With_Clamp_Then_Validate_All_Samples_Are_In_Range()
    {
        var series = TimeSeriesBuilder
            .New().UniformRandom(-100, 100)
            .Clamp(-42, 15)
            .Build();

        for (var i = 0; i < 1000; i++)
        {
            var sample = series.Sample();
            sample.Value.Should().BeInRange(-42, 15);
        }
    }

    [Fact]
    public void Build_From_Existing_TimeSeries_Then_Sample()
    {
        var s1 = TimeSeriesBuilder
            .New().Cosine()
            .Build();

        var s2 = TimeSeriesBuilder
            .FromTimeSeries(s1)
            .Add().GaussianRandom()
            .Build();

        var dataPoint = s2.Sample();

        dataPoint.OriginOffset.Should().BeGreaterOrEqualTo(TimeSpan.Zero);
    }

    [Fact]
    public void Build_A_New_Complex_TimeSeries_Then_Sample()
    {
        var series = TimeSeriesBuilder
            .New().Sine()
            .Add().Cosine()
            .Subtract().GaussianRandom()
            .Clamp(-1.0, 1.0)
            .Multiply().Sawtooth()
            .Pow(3.0)
            .SetTimeOrigin(TimeOrigin.UtcNow)
            .Build();

        var dataPoint = series.Sample();

        dataPoint.OriginOffset.Should().BeGreaterOrEqualTo(TimeSpan.Zero);
    }
}