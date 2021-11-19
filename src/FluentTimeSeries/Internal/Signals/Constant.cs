namespace FluentTimeSeries.Internal.Signals
{
    internal class Constant : AbstractFunction
    {
        private readonly double _constantValue;

        public Constant(double constantValue)
        {
            _constantValue = constantValue;
        }

        protected override double DoApply(double x)
        {
            return _constantValue;
        }
    }
}