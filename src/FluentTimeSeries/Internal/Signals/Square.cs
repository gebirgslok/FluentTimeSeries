using System;

namespace FluentTimeSeries.Internal.Signals
{
    internal class Square : AbstractPeriodicFunction
    {
        public Square(PeriodicFunctionConfiguration configuration) : base(configuration)
        {
        }

        protected override double DoApply(double t)
        {
            var tt = t + PhaseShift;
            var temp = tt / Period;
            var y = (2 * Math.Floor(temp) - Math.Floor(2 * temp));
            y *= 2;
            y += 1;
            y *= Amplitude; //[-a, a]
            y += VerticalShift; //[vs, a + vs]

            return y;
        }
    }
}