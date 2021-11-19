using FluentTimeSeries.Internal.Utils;

namespace FluentTimeSeries.Internal.Signals
{
    internal class UniformRandom : AbstractFunction
    {
        private readonly UniformRandomConfiguration _configuration;

        public UniformRandom(UniformRandomConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override double DoApply(double x)
        {
            return Rng.Next(_configuration.Min, _configuration.Max);
        }
    }
}