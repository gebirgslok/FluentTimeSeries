namespace FluentTimeSeries;

public interface ITimeSeriesTransformer
{
    double Transform(double prevValue);
}