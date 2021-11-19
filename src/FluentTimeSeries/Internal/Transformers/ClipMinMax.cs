using System;
using FluentTimeSeries;
using FluentTimeSeries.Internal.Transformers;

namespace Schulz.Ecc.SignalGenerator.Internal.Transformers
{
    internal class ClipMinMax : IScalarTransformer
    {
        private readonly ClipMinMaxConfiguration _configuration;

        public ClipMinMax(ClipMinMaxConfiguration configuration)
        {
            _configuration = configuration;
        }

        public double Transform(double current)
        {
            var max = _configuration.Max;
            var min = _configuration.Min;

            if (max.HasValue)
            {
                current = Math.Min(current, max.Value);
            }

            if (min.HasValue)
            {
                current = Math.Max(current, min.Value);
            }

            return current;
        }
    }
}