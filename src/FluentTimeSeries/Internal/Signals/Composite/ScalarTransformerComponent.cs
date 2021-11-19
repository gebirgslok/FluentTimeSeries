using System;
using FluentTimeSeries.Internal.Transformers;
using Schulz.Ecc.SignalGenerator.Internal.Transformers;

namespace FluentTimeSeries.Internal.Signals.Composite
{
    internal class ScalarTransformerComponent : IAccumulatorFunction
    {
        private readonly IScalarTransformer _transformer;

        public ScalarTransformerComponent(IScalarTransformer transformer)
        {
            _transformer = transformer;
        }

        public double Next(double current, DateTime timestamp)
        {
            return _transformer.Transform(current);
        }
    }
}