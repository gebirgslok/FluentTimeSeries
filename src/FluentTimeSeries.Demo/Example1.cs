using System;
using System.Linq;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.SKCharts;
using SkiaSharp;

namespace FluentTimeSeries.Demo;

internal static class Example1
{
    public static void Run()
    {
        var series = TimeSeriesBuilder
            .New().Square()
            .Add().Sine()
            .Subtract().Sawtooth(config =>
            {
                config.Period = TimeSpan.FromSeconds(3);
            })
            .Add().GaussianRandom(0, 0.1)
            .SetTimeOrigin(TimeOrigin.Now)
            .Build();

        var block = series.Block(200, 1.0 / 50);

        var cartesianChart = new SKCartesianChart
        {
            Width = 900,
            Height = 600,
            YAxes = new []{ new Axis()},
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

        var imagePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}/example1.png";
        cartesianChart.SaveImage(imagePath);
    }
}