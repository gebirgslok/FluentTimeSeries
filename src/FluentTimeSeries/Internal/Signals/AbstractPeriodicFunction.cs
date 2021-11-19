namespace FluentTimeSeries.Internal.Signals
{
    internal abstract class AbstractPeriodicFunction : AbstractFunction
    {
        private readonly PeriodicFunctionConfiguration _configuration;

        protected double Period => _configuration.Period;

        protected double PhaseShift => _configuration.PhaseShift;

        protected double Amplitude => _configuration.Amplitude;

        protected double VerticalShift => _configuration.VerticalShift;

        protected AbstractPeriodicFunction(PeriodicFunctionConfiguration configuration) : base(configuration.Origin)
        {
            _configuration = configuration;
        }
    }
}