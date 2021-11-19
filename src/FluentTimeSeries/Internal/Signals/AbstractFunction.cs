using System;

namespace FluentTimeSeries.Internal.Signals
{
    internal abstract class AbstractFunction : IFunction
    {
        private readonly DateTime? _maybeOrigin;

        private bool _isInitialized;
        private DateTime _origin;

        protected AbstractFunction(DateTime? maybeOrigin = null)
        {
            _maybeOrigin = maybeOrigin;
        }

        protected abstract double DoApply(double x);

        public double Apply(DateTime timestamp)
        {
            if (_isInitialized == false)
            {
                _origin = _maybeOrigin ?? timestamp;
                _isInitialized = true;
            }

            var t = (timestamp - _origin).TotalSeconds;
            var ft = DoApply(t);

            return ft;
        }
    }
}
