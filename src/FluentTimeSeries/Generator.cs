using FluentTimeSeries.Internal.Configuration;
using Schulz.Ecc.SignalGenerator.Internal.Configuration;

namespace FluentTimeSeries
{
    public static class Generator
    {
        public static IFunctionSelectionStage UseBase()
        {
            return TimeSeriesConfiguration.Base;
        }
    }
}
