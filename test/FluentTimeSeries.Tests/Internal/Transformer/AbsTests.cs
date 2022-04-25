using System;
using FluentAssertions;
using FluentTimeSeries.Internal.Transformer;
using Xunit;

namespace FluentTimeSeries.Tests.Internal.Transformer;

public class AbsTests
{
    [Theory]
    [InlineData(1.0)]
    [InlineData(-0.0)]
    [InlineData(1337.0)]
    [InlineData(-1.0)]
    [InlineData(double.MaxValue)]
    [InlineData(double.MinValue)]
    [InlineData(-double.Epsilon)]
    [InlineData(double.Epsilon)]
    public void Transform_Always_Returns_Abs(double prev)
    {
        var transformer = new Abs();
        transformer.Transform(prev).Should().Be(Math.Abs(prev));
    }
}
