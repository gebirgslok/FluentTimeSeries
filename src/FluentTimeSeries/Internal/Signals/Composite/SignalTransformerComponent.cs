using System;
using FluentTimeSeries.Internal.Transformers;

namespace FluentTimeSeries.Internal.Signals.Composite
{
    internal class AccumlatorFunction : IAccumulatorFunction
    {

    }

    internal class SignalTransformerComponent : IAccumulatorFunction
    {
        private readonly IFunctionTransformer _transformer;

        private readonly IFunction _signal;

        public SignalTransformerComponent(IFunction signal, IFunctionTransformer transformer)
        {
            _signal = signal;
            _transformer = transformer;
        }

        public double Next(double current, DateTime timestamp)
        {
            var next = _transformer.Transform(_signal, current, timestamp);
            return next;
        }
    }
}