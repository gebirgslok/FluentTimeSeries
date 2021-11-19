using System;
using System.Collections.Generic;
using System.Linq;
using FluentTimeSeries.Internal.Signals;
using FluentTimeSeries.Internal.Signals.Composite;
using FluentTimeSeries.Internal.Transformers;
using Schulz.Ecc.SignalGenerator.Internal.Signals;
using Schulz.Ecc.SignalGenerator.Internal.Transformers;

namespace FluentTimeSeries.Internal.Configuration
{
    internal class TimeSeriesConfiguration : ITimeSeriesConfiguration
    {
        private readonly IList<ICompositeSignalComponent> _signalComponents = new List<ICompositeSignalComponent>();
        private IFunctionTransformer? _currentTransformer;

        public static TimeSeriesConfiguration Base => new TimeSeriesConfiguration(new Base());

        private TimeSeriesConfiguration(IFunctionTransformer transformer)
        {
            _currentTransformer = transformer;
        }

        private ITimeSeriesConfiguration UseSignal(IFunction signal)
        {
            if (_currentTransformer == null)
            {
                throw new InvalidOperationException("");
            }

            _signalComponents.Add(new SignalTransformerComponent(signal, _currentTransformer));

            //Reset the transformer
            _currentTransformer = null;

            return this;
        }

        private ITimeSeriesConfiguration UseSignal<TConfig>(Action<TConfig>? configure, Func<TConfig, IFunction> createSignal) where TConfig : new()
        {
            var configuration = new TConfig();
            configure?.Invoke(configuration);
            return UseSignal(createSignal(configuration));
        }

        public ITimeSeries Build()
        {
            return new CompositeTimeSeries(_signalComponents.ToArray());
        }

        public IFunctionSelectionStage Add()
        {
            _currentTransformer = new Add();
            return this;
        }

        public ITimeSeriesConfiguration Add(double constantValue)
        {
            _signalComponents.Add(new SignalTransformerComponent(new Constant(constantValue), new Add()));
            return this;
        }

        public IFunctionSelectionStage Subtract()
        {
            _currentTransformer = new Subtract();
            return this;
        }

        public IFunctionSelectionStage Multiply()
        {
            _currentTransformer = new Multiply();
            return this;
        }

        public ITimeSeriesConfiguration Multiply(double constantValue)
        {
            _signalComponents.Add(new SignalTransformerComponent(new Constant(constantValue), new Multiply()));
            return this;
        }

        public ITimeSeriesConfiguration ClipMinMax(Action<ClipMinMaxConfiguration>? configure = null)
        {
            var configuration = new ClipMinMaxConfiguration();
            configure?.Invoke(configuration);
            _signalComponents.Add(new ScalarTransformerComponent(new ClipMinMax(configuration)));
            return this;
        }

        public ITimeSeriesConfiguration Pow(double exponent)
        {
            _signalComponents.Add(new ScalarTransformerComponent(new Pow(exponent)));
            return this;
        }

        void ITimeSeriesConfiguration.SetCurrentTransformer(IFunctionTransformer transformer)
        {
            if (_currentTransformer != null)
            {
                //TODO throw new exception
            }

            _currentTransformer = transformer;
        }

        public ITimeSeriesConfiguration Sine(Action<PeriodicFunctionConfiguration>? configure = null) =>
            UseSignal(configure, c => new Sine(c));

        public ITimeSeriesConfiguration Cosine(Action<PeriodicFunctionConfiguration>? configure = null)
        {
            throw new NotImplementedException();
        }

        public ITimeSeriesConfiguration UniformRandom(Action<UniformRandomConfiguration>? configure = null) =>
            UseSignal(configure, c => new UniformRandom(c));

        public ITimeSeriesConfiguration GaussianRandom(Action<GaussianRandomConfiguration>? configure = null) =>
            UseSignal(configure, c => new GaussianRandom(c));

        public ITimeSeriesConfiguration Sawtooth(Action<PeriodicFunctionConfiguration>? configure = null) =>
            UseSignal(configure, c => new Sawtooth(c));

        public ITimeSeriesConfiguration Square(Action<PeriodicFunctionConfiguration>? configure = null) =>
            UseSignal(configure, c => new Square(c));

        public ITimeSeriesConfiguration Triangle(Action<PeriodicFunctionConfiguration>? configure = null) =>
            UseSignal(configure, c => new Triangle(c));

        public ITimeSeriesConfiguration Constant(double value) => UseSignal(new Constant(value));
    }
}