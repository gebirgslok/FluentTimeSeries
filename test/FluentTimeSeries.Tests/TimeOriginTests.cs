using System;
using FluentAssertions;
using Xunit;

namespace FluentTimeSeries.Tests;

public class TimeOriginTests
{
    [Fact]
    public void UtcNow_Then_GetNow_Returns_Date_In_Utc()
    {
        var utcOrigin = TimeOrigin.UtcNow;
        var now = utcOrigin.GetNow();
        now.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }
}