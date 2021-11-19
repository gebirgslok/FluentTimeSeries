using System;

namespace FluentTimeSeries
{
    public class PeriodicFunctionConfiguration
    {
        public double Amplitude { get; set; } = 1.0;

        public double Period { get; set; } = 1.0;

        public double PhaseShift { get; set; } = 0.0;

        public double VerticalShift { get; set; } = 0.0;

        public DateTime? Origin { get; set; } = null;
    }
}