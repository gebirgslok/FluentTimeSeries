namespace FluentTimeSeries.Internal;

internal class TimeSeriesTransformerComponent : ITimeSeriesComponent
{
    private readonly ITimeSeriesTransformer _transformer;

    public TimeSeriesTransformerComponent(ITimeSeriesTransformer transformer)
    {
        _transformer = transformer;
    }

    public void Next(ICurrentSampleContext context)
    {
    }
}