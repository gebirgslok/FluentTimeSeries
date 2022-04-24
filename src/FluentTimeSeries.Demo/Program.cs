using System;

namespace FluentTimeSeries.Demo;

internal static class Program
{
    public static void Main(string[] args)
    {
        var timeSeries = TimeSeriesBuilder 
            //Use a sine with period = 10s as base
            .New().Sine(TimeSpan.FromSeconds(10))
            .Add().Constant(42.1337)
            .Abs()
            .Multiply().Sine(TimeSpan.FromSeconds(1))
            .Subtract().UniformRandom(-10.0, 25.4)
            .Pow(3.0)
            .Add().GaussianRandom(configure =>
            {
                configure.Mean = 0.0;
                configure.StdDev = 3.0;
            })
            .Clamp(configure =>
            {
                configure.Min = 1.0;
                configure.Max = 2.0;
            })
            .WithUtcNowAsOrigin()
            .Build();

        var samplePoint = timeSeries.Sample();
    }
}