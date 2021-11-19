using System;

namespace FluentTimeSeries.Internal.Signals
{
    internal class Triangle : AbstractPeriodicFunction
    {
        public Triangle(PeriodicFunctionConfiguration configuration) : base(configuration)
        {
        }

        protected override double DoApply(double t)
        {
            var tt = t + PhaseShift;
            var temp = tt / Period;
            var y = Math.Abs(2 * (temp - Math.Floor(temp + 0.5)));
            y *= 2;
            y -= 1;
            y *= Amplitude; //[-a, a]
            y += VerticalShift; //[vs, a + vs]

            return y;
        }
    }
}