using System;
using System.Linq;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.SKCharts;
using SkiaSharp;

namespace FluentTimeSeries.Demo;

internal static class Example2
{
    public static void Run()
    {
        var series = TimeSeriesBuilder
            .New().Sine()
            .Add().Cosine(config =>
            {
                config.Amplitude = 3.0;
                config.VerticalShift = -2.0;
            })
            .Subtract().Sawtooth(config =>
            {
                config.Period = TimeSpan.FromSeconds(2);
            })
            .Add().GaussianRandom(mean: 0.0, stdDev: 0.1)
            .SetTimeOrigin(TimeOrigin.UtcNow)
            .Build();

        var block = series.Block(200, 1.0 / 50);

        var cartesianChart = new SKCartesianChart
        {
            Width = 900,
            Height = 600,
            YAxes = new[] { new Axis() },
            Series = new ISeries[]
            {
                new LineSeries<double>
                {
                    Values = block.Select(x => x.Value),
                    GeometrySize = 6,
                    Stroke = new SolidColorPaint(SKColor.Parse("#c1c1c1"), 2f),
                    Fill = null,
                }
            }
        };

        var imagePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}/example2.png";
        cartesianChart.SaveImage(imagePath);
    }
}