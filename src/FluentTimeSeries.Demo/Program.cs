using System;
using System.Linq;

namespace FluentTimeSeries.Demo;

internal static class Program
{
    private static readonly Example[] _examples = {
        new(1, "y(t) = sin(t) + cos(t)", Example1.Run),
        new(2, "y(t) = square(t) + GaussianNoise", Example1.Run),
    };

    public static void Main(string[] args)
    {
        Console.WriteLine("Please select example to run");

        foreach (var e in _examples)
        {
            Console.WriteLine($"{e.Id}: {e.Name}");
        }

        Console.Write("Example ID:");
        var input = Console.ReadLine();

        if (!int.TryParse(input, out var id) || _examples.FirstOrDefault(e => e.Id == id) == null)
        {
            //Invalid
            return;
        }

        var example = _examples.First(e => e.Id == id);
        example.Run();


        //var timeSeries = TimeSeriesBuilder 
        //    //Use a sine with period = 10s as base
        //    .New().Sine(TimeSpan.FromSeconds(10))
        //    .Add().Constant(42.1337)
        //    .Abs()
        //    .Multiply().Sine(TimeSpan.FromSeconds(1))
        //    .Subtract().UniformRandom(-10.0, 25.4)
        //    .Pow(3.0)
        //    .Add().GaussianRandom(configure =>
        //    {
        //        configure.Mean = 0.0;
        //        configure.StdDev = 3.0;
        //    })
        //    .Clamp(configure =>
        //    {
        //        configure.Min = 1.0;
        //        configure.Max = 2.0;
        //    })
        //    .WithUtcNowAsOrigin()
        //    .Build();

        //var samplePoint = timeSeries.Sample();
    }
}