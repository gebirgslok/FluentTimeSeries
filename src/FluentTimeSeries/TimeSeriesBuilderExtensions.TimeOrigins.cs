namespace FluentTimeSeries;

public static partial class TimeSeriesBuilderExtensions
{
    public static ICanBuildTimeSeries WithUtcNowAsOrigin(this IValueGeneratorSelectionStage stage)
    {
        return stage.SetTimeOrigin(TimeOrigin.UtcNow);
    }
}