using System;

namespace FluentTimeSeries.Internal.Transformers
{
    internal interface IValueTransformer
    {
        double Transform(Func<double> getCurrent);
    }

    internal interface IScalarTransformer
    {
        double Transform(double current);
    }
}