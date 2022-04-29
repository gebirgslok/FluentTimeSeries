﻿using System;
using System.Linq;

namespace FluentTimeSeries.Demo;

internal static class Program
{
    private static readonly Example[] _examples = {
        new(1, "y(t) = sin(t) + cos(t)", Example1.Run),
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
            Console.WriteLine($"Invalid input '{input}'");
            return;
        }

        var example = _examples.First(e => e.Id == id);
        example.Run();
    }
}