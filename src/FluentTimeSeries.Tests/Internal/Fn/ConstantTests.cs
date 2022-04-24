using System;
using FluentAssertions;
using Xunit;

namespace FluentTimeSeries.Tests.Internal.Fn;

public class ConstantTests
{
    [Fact]
    public void Apply_Always_Returns_Constant_Value()
    {
        const double value = 42.1337;
        var constantFn = new FluentTimeSeries.Internal.Fn.Constant(value);

        var now = DateTime.Now;

        for (var i = 0; i < 100; i++)
        {
            var t = now.AddSeconds(i) - now;
            constantFn.Apply(t).Should().Be(value);
        }
    }
}