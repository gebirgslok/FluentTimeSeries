using System;
using FluentTimeSeries.Internal.Utils;

namespace FluentTimeSeries.Internal.Signals
{
    internal class GaussianRandom : AbstractFunction
    {
        private readonly GaussianRandomConfiguration _configuration;

        public GaussianRandom(GaussianRandomConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override double DoApply(double x)
        {
            //Code taken from https://stackoverflow.com/questions/218060/random-gaussian-variables
            var u1 = 1.0 - Rng.NextDouble();
            var u2 = 1.0 - Rng.NextDouble();

            var randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                                Math.Sin(2.0 * Math.PI * u2);
            var randNormal =
                _configuration.Mean + _configuration.StdDev * randStdNormal;

            return randNormal;
        }
    }
}