using System;

namespace FluentTimeSeries.Internal.Signals
{
    internal class Sine : AbstractPeriodicFunction
    {
        public Sine(PeriodicFunctionConfiguration configuration) : base(configuration)
        {
        }

        protected override double DoApply(double x)
        {
            var a = Amplitude;
            var b = 2 * Math.PI / Period;
            var c = PhaseShift;
            var d = VerticalShift;
            return a * Math.Sin(b * (x + c)) + d;
        }
    }
}
