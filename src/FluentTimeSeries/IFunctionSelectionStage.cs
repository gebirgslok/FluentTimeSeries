using System;
using Schulz.Ecc.SignalGenerator;

namespace FluentTimeSeries
{
    public interface IFunctionSelectionStage
    {
        ITimeSeriesConfiguration Sine(Action<PeriodicFunctionConfiguration>? configure = null);

        ITimeSeriesConfiguration UniformRandom(Action<UniformRandomConfiguration>? configure = null);

        ITimeSeriesConfiguration GaussianRandom(Action<GaussianRandomConfiguration>? configure = null);

        ITimeSeriesConfiguration Sawtooth(Action<PeriodicFunctionConfiguration>? configure = null);

        ITimeSeriesConfiguration Square(Action<PeriodicFunctionConfiguration>? configure = null);

        ITimeSeriesConfiguration Triangle(Action<PeriodicFunctionConfiguration>? configure = null);

        ITimeSeriesConfiguration Constant(double value);
    }
}