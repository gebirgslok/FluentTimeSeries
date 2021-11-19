using System;

namespace FluentTimeSeries.Internal.Transformers
{
    internal interface IAccumulatorFunction
    {
        double Next(double current, DateTime timestamp);
    }
}