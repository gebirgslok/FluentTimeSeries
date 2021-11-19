using System;
using FluentTimeSeries.Internal.Signals;
using FluentTimeSeries.Internal.Transformers;
using Schulz.Ecc.SignalGenerator.Internal.Signals;

namespace Schulz.Ecc.SignalGenerator.Internal.Transformers
{
    internal class Base : IFunctionTransformer
    {
        public double Transform(IFunction signal, double current, DateTime timestamp)
        {
            return signal.NextValue(timestamp);
        }
    }
}