using System;

namespace FluentTimeSeries;

public interface IFt
{
    double Apply(TimeSpan t);
}