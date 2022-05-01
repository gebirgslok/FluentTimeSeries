using System;
using FluentTimeSeries.Internal.Transformer;

namespace FluentTimeSeries;

public static partial class TimeSeriesBuilderExtensions
{
    public static IValueGeneratorSelectionStage MultiplyBy(this IValueGeneratorSelectionStage stage,
        double factor)
    {
        return stage.SetTransformer(new MultiplyBy(factor));
    }

    public static IValueGeneratorSelectionStage Pow(this IValueGeneratorSelectionStage stage, 
        double exponent = 2.0)
    {
        return stage.SetTransformer(new Pow(exponent));
    }

    public static IValueGeneratorSelectionStage Abs(this IValueGeneratorSelectionStage stage)
    {
        return stage.SetTransformer(new Abs());
    }

    public static IValueGeneratorSelectionStage Clamp(this IValueGeneratorSelectionStage stage,
        double? min, double? max)
    {
        var p = new ClampConfiguration
        {
            Min = min,
            Max = max
        };
        return stage.SetTransformer(new Clamp(p));
    }

    public static IValueGeneratorSelectionStage Clamp(this IValueGeneratorSelectionStage stage,
        Action<ClampConfiguration>? configure = null)
    {
        var p = new ClampConfiguration();
        configure?.Invoke(p);
        return stage.SetTransformer(new Clamp(p));
    }
}