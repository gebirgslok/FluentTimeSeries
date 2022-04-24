namespace FluentTimeSeries.Internal;

internal class TimeSeriesAggregatorComponent : ITimeSeriesComponent
{
    private readonly IFt _ft;
    private readonly ITimeSeriesAggregator _aggregator;

    public TimeSeriesAggregatorComponent(IFt ft, ITimeSeriesAggregator aggregator)
    {
        _ft = ft;
        _aggregator = aggregator;
    }

    public void Next(ICurrentSampleContext context)
    {
        var prev = context.CurrentValue;
        var t = context.RelativeTimestamp;
        var nextValue = _aggregator.Aggregate(prev, _ft, t);
        context.SetNextValue(nextValue);
    }
}