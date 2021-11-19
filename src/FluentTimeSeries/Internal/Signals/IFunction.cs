using System;

namespace FluentTimeSeries.Internal.Signals
{
    internal interface IFunction
    {
        double Apply(DateTime timestamp);
    }
}