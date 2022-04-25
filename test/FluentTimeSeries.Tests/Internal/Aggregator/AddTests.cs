using System;
using FakeItEasy;
using FluentAssertions;
using FluentTimeSeries.Internal.Aggregator;
using Xunit;

namespace FluentTimeSeries.Tests.Internal.Aggregator;

public class AddTests
{
    [Theory]
    [InlineData(15.42, 42.1337)]
    [InlineData(15.42, -42.1337)]
    [InlineData(-15.42, 0.0)]
    [InlineData(-15.42, -121.21)]
    public void Add_Adds_Result_Of_Fn_To_Prev(double prevValue, double ftOut)
    {
        var add = new Add();
        var t = TimeSpan.FromSeconds(10);
        var fn = A.Fake<IFt>();
        A.CallTo(() => fn.Apply(t)).Returns(ftOut);

        var result = add.Aggregate(prevValue, fn, t);

        result.Should().BeApproximately(ftOut + prevValue, double.Epsilon);
    }
}