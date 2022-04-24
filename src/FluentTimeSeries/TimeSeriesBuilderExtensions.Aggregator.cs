using FluentTimeSeries.Internal.Aggregator;

namespace FluentTimeSeries;

public static partial class TimeSeriesBuilderExtensions
{
    public static ITimeSeriesSelectionStage Subtract(this IValueGeneratorSelectionStage stage)
    {
        return stage.SetAggregator(new Subtract());
    }

    public static ITimeSeriesSelectionStage Add(this IValueGeneratorSelectionStage stage)
    {
        return stage.SetAggregator(new Add());
    }

    public static ITimeSeriesSelectionStage Multiply(this IValueGeneratorSelectionStage stage)
    {
        return stage.SetAggregator(new Multiply());
    }
}