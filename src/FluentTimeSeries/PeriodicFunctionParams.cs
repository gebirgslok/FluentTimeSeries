using System;

namespace FluentTimeSeries;

public class PeriodicFunctionParams
{
    public double Amplitude { get; set; } = 1.0;

    public TimeSpan Period { get; set; } = TimeSpan.FromSeconds(1);

    public TimeSpan PhaseShift { get; set; } = TimeSpan.FromSeconds(0);

    public double VerticalShift { get; set; }
}