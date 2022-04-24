using System;

namespace FluentTimeSeries.Internal;

internal interface ICurrentSampleContext
{
    double CurrentValue { get; }

    TimeSpan RelativeTimestamp { get; }

    void SetNextValue(double value);
}