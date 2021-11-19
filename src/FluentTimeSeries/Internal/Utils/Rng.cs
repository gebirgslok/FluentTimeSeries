using System;

namespace FluentTimeSeries.Internal.Utils
{
    internal static class Rng
    {
        private static readonly Random _random = new Random();

        public static double Next(double min, double max) => (max - min) * _random.NextDouble() + min;

        public static double NextDouble() => _random.NextDouble();
    }
}
