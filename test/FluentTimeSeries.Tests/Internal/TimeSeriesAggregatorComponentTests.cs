using System;
using FakeItEasy;
using FluentAssertions;
using FluentTimeSeries.Internal;
using Xunit;

namespace FluentTimeSeries.Tests.Internal;

public class TimeSeriesAggregatorComponentTests
{
    [Fact]
    public void Next_Delegates_Calculation_Of_Next_Value_To_Aggregator()
    {
        var ft = A.Fake<IFt>();
        var t = TimeSpan.FromSeconds(1);
        const double prev = 1.0;
        const double ftOut = 2.0;
        const double next = 3.0;
        A.CallTo(() => ft.Apply(t)).Returns(ftOut);

        var aggregator = A.Fake<ITimeSeriesAggregator>();
        A.CallTo(() => aggregator.Aggregate(prev, ft, t)).Returns(next);

        var aggregatorElement = new TimeSeriesAggregatorComponent(ft, aggregator);

        var context = new CurrentSampleContext(t, DateTime.Now);
        context.SetNextValue(prev);

        aggregatorElement.Next(context);

        context.CurrentValue.Should().Be(next);
    }
}
