//using System;
//using FluentTimeSeries.Internal.Transformers;

//namespace FluentTimeSeries;

//public interface ITimeSeriesBuilder : IFunctionSelectionStage
//{
//    ITimeSeries Build();

//    IFunctionSelectionStage Add();

//    ITimeSeriesBuilder Add(double summand);

//    IFunctionSelectionStage Subtract();

//    IFunctionSelectionStage Multiply();

//    ITimeSeriesBuilder Multiply(double factor);

//    ITimeSeriesBuilder ClipMinMax(Action<ClipMinMaxConfiguration>? configure = null);

//    ITimeSeriesBuilder Pow(double exponent);

//    internal void SetCurrentTransformer(IFunctionTransformer transformer);
//}