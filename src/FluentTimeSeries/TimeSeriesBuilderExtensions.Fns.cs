using System;
using FluentTimeSeries.Internal.Fn;

namespace FluentTimeSeries;

public static partial class TimeSeriesBuilderExtensions
{
    public static IValueGeneratorSelectionStage GaussianRandom(this ITimeSeriesSelectionStage stage,
        double mean, double stdDev)
    {
        var p = new GaussianRandomConfiguration
        {
            Mean = mean,
            StdDev = stdDev
        };
        return stage.SetFn(new GaussianRandom(p));
    }

    public static IValueGeneratorSelectionStage GaussianRandom(this ITimeSeriesSelectionStage stage,
        Action<GaussianRandomConfiguration>? configure = null)
    {
        var p = new GaussianRandomConfiguration();
        configure?.Invoke(p);
        return stage.SetFn(new GaussianRandom(p));
    }

    public static IValueGeneratorSelectionStage UniformRandom(this ITimeSeriesSelectionStage stage,
        double min, double max)
    {
        var p = new UniformRandomConfiguration
        {
            Max = max,
            Min = min,
        };
        return stage.SetFn(new UniformRandom(p));
    }

    public static IValueGeneratorSelectionStage UniformRandom(this ITimeSeriesSelectionStage stage,
        Action<UniformRandomConfiguration>? configure = null)
    {
        var p = new UniformRandomConfiguration();
        configure?.Invoke(p);
        return stage.SetFn(new UniformRandom(p));
    }

    public static IValueGeneratorSelectionStage Constant(this ITimeSeriesSelectionStage stage,
        double constantValue)
    {
        return stage.SetFn(new Constant(constantValue));
    }

    public static IValueGeneratorSelectionStage Sine(this ITimeSeriesSelectionStage stage, 
        TimeSpan period, double a = 1.0, double verticalShift = 0.0, TimeSpan? phaseShift = null)
    {
        var p = new PeriodicFunctionParams
        {
            Amplitude = a,
            Period = period,
            VerticalShift = verticalShift,
            PhaseShift = phaseShift ?? TimeSpan.FromSeconds(0)
        };
        return stage.SetFn(new Sine(p));
    }

    public static IValueGeneratorSelectionStage Sine(this ITimeSeriesSelectionStage stage, 
        Action<PeriodicFunctionParams>? configure = null)
    {
        var p = new PeriodicFunctionParams();
        configure?.Invoke(p);
        return stage.SetFn(new Sine(p));
    }

    public static IValueGeneratorSelectionStage Cosine(this ITimeSeriesSelectionStage stage,
        TimeSpan period, double a = 1.0, double verticalShift = 0.0, TimeSpan? phaseShift = null)
    {
        var p = new PeriodicFunctionParams
        {
            Amplitude = a,
            Period = period,
            VerticalShift = verticalShift,
            PhaseShift = phaseShift ?? TimeSpan.FromSeconds(0)
        };
        return stage.SetFn(new Cosine(p));
    }

    public static IValueGeneratorSelectionStage Cosine(this ITimeSeriesSelectionStage stage,
        Action<PeriodicFunctionParams>? configure = null)
    {
        var p = new PeriodicFunctionParams();
        configure?.Invoke(p);
        return stage.SetFn(new Cosine(p));
    }

    public static IValueGeneratorSelectionStage Triangle(this ITimeSeriesSelectionStage stage,
        Action<PeriodicFunctionParams>? configure = null)
    {
        var p = new PeriodicFunctionParams();
        configure?.Invoke(p);
        return stage.SetFn(new Triangle(p));
    }

    public static IValueGeneratorSelectionStage Square(this ITimeSeriesSelectionStage stage,
        Action<PeriodicFunctionParams>? configure = null)
    {
        var p = new PeriodicFunctionParams();
        configure?.Invoke(p);
        return stage.SetFn(new Square(p));
    }

    public static IValueGeneratorSelectionStage Sawtooth(this ITimeSeriesSelectionStage stage,
        Action<PeriodicFunctionParams>? configure = null)
    {
        var p = new PeriodicFunctionParams();
        configure?.Invoke(p);
        return stage.SetFn(new Sawtooth(p));
    }
}