namespace FluentTimeSeries.Internal;

internal interface ITimeSeriesComponent
{
    void Next(ICurrentSampleContext context);
}