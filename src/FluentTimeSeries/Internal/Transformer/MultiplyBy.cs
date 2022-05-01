namespace FluentTimeSeries.Internal.Transformer;

internal class MultiplyBy : ITimeSeriesTransformer
{
    private readonly double _factor;

    public MultiplyBy(double factor)
    {
        _factor = factor;
    }

    public double Transform(double prevValue)
    {
        return prevValue * _factor;
    }
}