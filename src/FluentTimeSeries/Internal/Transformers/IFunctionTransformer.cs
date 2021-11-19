using System;
using FluentTimeSeries.Internal.Signals;

namespace FluentTimeSeries.Internal.Transformers
{
    internal interface IFunctionTransformer
    {
        double Transform(IFunction signal, double current, DateTime timestamp);
    }
}