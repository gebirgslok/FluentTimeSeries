using System;
using FluentTimeSeries.Internal.Transformers;
using Schulz.Ecc.SignalGenerator;
using Schulz.Ecc.SignalGenerator.Internal.Transformers;

namespace FluentTimeSeries
{
    public interface ITimeSeriesConfiguration : IFunctionSelectionStage
    {
        ITimeSeries Build();

        IFunctionSelectionStage Add();

        ITimeSeriesConfiguration Add(double summand);

        IFunctionSelectionStage Subtract();

        IFunctionSelectionStage Multiply();

        ITimeSeriesConfiguration Multiply(double factor);

        ITimeSeriesConfiguration ClipMinMax(Action<ClipMinMaxConfiguration>? configure = null);

        ITimeSeriesConfiguration Pow(double exponent);

        internal void SetCurrentTransformer(IFunctionTransformer transformer);
    }
}