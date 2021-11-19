using System;

namespace FluentTimeSeries.Internal.Signals.Composite
{
    internal interface IAccumulatorFunction
    {
        double Next(double current, DateTime timestamp);
    }
}