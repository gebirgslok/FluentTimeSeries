using FluentAssertions;
using FluentTimeSeries.Internal;
using FluentTimeSeries.Internal.Transformer;
using Xunit;

namespace FluentTimeSeries.Tests.Internal.Transformer;

public class MultiplyByTests
{
    [Theory]
    [InlineData(1.0)]
    [InlineData(-0.0)]
    [InlineData(1337.0)]
    [InlineData(-1.0)]
    [InlineData(42.15)]
    public void Transform_Returns_Product(double prev)
    {
        var factor = Rng.Next(0.5, 2.5);
        var transformer = new MultiplyBy(factor);
        transformer.Transform(prev).Should().BeApproximately(prev * factor, TestDefaults.ComparisonEpsilon);
    }
}