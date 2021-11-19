using System;

namespace FluentTimeSeries.Internal.Signals
{
    internal class Sawtooth : AbstractPeriodicFunction
    {
        public Sawtooth(PeriodicFunctionConfiguration configuration) : base(configuration)
        {
        }

        protected override double DoApply(double t)
        {
            var tt = t + PhaseShift;
            var temp = tt / Period;
            var y = temp - Math.Floor(0.5 + temp); //[-.5, .5]
            y *= 2; //[-1, 1]
            y *= Amplitude; //[-a, a]
            y += VerticalShift; //[vs, a + vs]

            return y;
        }
    }
}