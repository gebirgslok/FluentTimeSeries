

[![NuGet](https://img.shields.io/nuget/v/FluentTimeSeries?color=blue&logo=NuGet)](https://www.nuget.org/packages/FluentTimeSeries/)
[![Build Status](https://dev.azure.com/jeisenbach/FluentTimeSeries/_apis/build/status/gebirgslok.FluentTimeSeries?branchName=main)](https://dev.azure.com/jeisenbach/FluentTimeSeries/_build/latest?definitionId=4&branchName=main)
![Azure DevOps coverage](https://img.shields.io/azure-devops/coverage/jeisenbach/FluentTimeSeries/4?logo=Codecov)
# FluentTimeSeries
## Introduction
FluentTimeSeries is a small utility library with a Fluent-API that allows you to design and utilize complex time series with just a few lines of code.
```csharp
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
```
The above code generates the following time series:
```
s(t) = sin(t') + (3 * cos(t') - 2) - sawtooth(2 * t') + noise(t')
with t' = t - t0
```

> Sawtooth wave on [Wikipedia](https://en.wikipedia.org/wiki/Sawtooth_wave)

Then, generate sample points or blocks of data points:
```csharp
//timestamp = DateTime.UtcNow...
var samplePoint = series.Sample(); 
//...or provide your own timestamp
var samplePoint = series.Sample(new DateTime(2000, 1, 1, 0, 0, 0));

van samplingIntervalSeconds = 0.05;
//startTimestamp = DateTime.UtcNow...
var dataPoints = series.Block(1000, samplingIntervalSeconds);
//...or provide your own timestamp
var dataPoints = series.Block(1000, samplingIntervalSeconds, new DateTime(2000, 1, 1, 0, 0, 0));
```
### And now...?
I wrote this library to be able to *fast-and-easy* create data to
 - mock sensors and IoT devices
 - prototype and mock-up realistic UIs, in particular dashboards
 - write *Unit* and *Integration* tests
 - initialize backends / databases for *Staging* and *Test* environments

Of course, the library is not limited to the above-mentioned purposes, the imagination of us developers is, as we all know, unlimited. :)

### What it is not
FluentTimeSeries is **not** meant as a (scientific) signal processing library. So don't expect stuff like *FFT*s or *Convolution*s.

## Get started
### Install NuGet package
#### Package Manager Console
```
Install-Package FluentTimeSeries
```
#### .NET CLI
```
dotnet add package FluentTimeSeries
```
### TimeSeriesBuilder
#### New
```csharp
//Creates a new builder with 'Sine' as the base function
var builder = TimeSeriesBuilder.New().Sine();
```
#### Aggregate additional functions: Add, Subtract, Multiply
```csharp
//Adds the 'Cosine' function -> s(t) = sin(t) + cos(t) 
builder = builder.Add().Cosine();

//Subtracts the 'Square' function -> s(t) = (sin(t) + cos(t)) - square(t)
builder = builder.Subtract().Square();

//Multiplies the current value with the 'Sawtooth' function 
//-> s(t) = ((sin(t) + cos(t)) - square(t)) * sawtooth(t)
builder = builder.Multiply().Sawtooth();
```
> Square wave on [Wikipedia](https://en.wikipedia.org/wiki/Square_wave)
#### Add transformations: Abs, Clamp, Pow, MultiplyBy
```csharp
//Take abs (|s(t)|) of the current value 
builder = builder.Abs();

//Clamp current value between -0.5 and +0.5 (inclusive)
builder = builder.Clamp(-0.5, 0.5);

//Apply power=3 to the current value (s(t)^3)
builder = builder.Pow(3);

//Multiply current value by a constant factor (s(t) * c)
builder = builder.MultiplyBy(42);
```
#### Set time origin
You can optionally set the time origin of the series that will be utilized when generating data. 

> :information_source: Defaults to `DateTime.Now` when no time origin is set.

```csharp
//Creates a new builder with 'Sine' as the base function
builder = builder.SetTimeOrigin(TimeOrigin.UtcNow);
```
#### From existing time series
You can configure a new time series based on an existing one. Therefore, use the static factory method `FromTimeSeries` on the `TimeSeriesBuilder` class.
```csharp
var s1 = GetExistingSeries(); //Existing TimeSeries instance
var s2 = TimeSeriesBuilder
	.FromTimeSeries(s1)
	//Aggragete additional functions and apply additionals transformations here, e.g.
	.Add().Cosine()
	.Subtract().Sawtooth()
	.Abs()
	.Add().UniformRandom()
	.Build();
```
The `FromTimeSeries` accepts a second *optinal* argument - `timeOrigin` - that lets you overwrite the time origin of the base series. 

#### Build
Once everything is configured, call `Build()` to build the time series instance.
```csharp
var series = builder.Build();
```
### TimeSeries
The `TimeSeries` object comes with two methods to generate data: `Sample` and `Block`.
#### Sample
Use the `Sample` method to generate a *single* data point (e.g. the 'next' measurement of a sensor).
A typical use case might be a timer which simulates a sensor and produces a new measurement (e.g. temperature, voltage, power, ...) every fixed interval.
```csharp
// Use DateTime.Now (DateTime.UtcNow if the TimeOrigin is TimeOrigin.UtcNow) 
// as the timestamp ...
var samplePoint = series.Sample();

//...or provide a timestamp
var samplePoint = series.Sample(new DateTime(2000, 1, 1, 0, 0, 0);
```
#### Block
Use the `Block` method to generate a block of data points. Use the optional `startTimestamp` argument to specify timestamp of first data point. If not specified the timestamp of the first point will be the time origin of the series.
```csharp
// Generates data points every 0.1s for 100s = 1000 data points...
var dataPoints = series.Block(TimeSpan.FromSeconds(100), 0.1);

//...or specify the #of data points directly
var dataPoints = series.Block(1000, 0.1);
```

### Extend the configuration builder
#### Add custom functions
tbd
#### Add custom transformers
tbd

