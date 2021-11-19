using System;
using FluentTimeSeries.Internal.Signals;

namespace FluentTimeSeries.Internal.Transformers
{
    internal class Add : IFunctionTransformer
    {
        public double Transform(IFunction signal, double current, DateTime timestamp)
        {
            return current + signal.NextValue(timestamp);
        }
    }
}