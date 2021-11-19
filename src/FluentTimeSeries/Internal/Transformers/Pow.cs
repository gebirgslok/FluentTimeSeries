using System;
using FluentTimeSeries.Internal.Transformers;

namespace Schulz.Ecc.SignalGenerator.Internal.Transformers
{
    internal class Pow : IScalarTransformer
    {
        private readonly double _exponent;

        public Pow(double exponent)
        {
            _exponent = exponent;
        }

        public double Transform(double current)
        {
            return Math.Pow(current, _exponent);
        }
    }
}