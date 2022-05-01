using System;
using System.Collections.Generic;
using System.Linq;
using FluentTimeSeries.Internal;

namespace FluentTimeSeries;

public interface ITimeSeriesSelectionStage
{
    IValueGeneratorSelectionStage SetFn(IFt fx);
}

public interface IValueGeneratorSelectionStage : ICanBuildTimeSeries
{
    IValueGeneratorSelectionStage SetTransformer(ITimeSeriesTransformer transformer);

    ITimeSeriesSelectionStage SetAggregator(ITimeSeriesAggregator combiner);

    ICanBuildTimeSeries SetTimeOrigin(TimeOrigin timeOrigin);
}

public interface ICanBuildTimeSeries
{
    TimeSeries Build();
}

public class TimeSeriesBuilder : IValueGeneratorSelectionStage, ITimeSeriesSelectionStage
{
    [Flags]
    private enum AllowedTimeSeriesBuilderActions
    {
        None = 0x00,
        SetFn = 0x01,
        SetTransformer = 0x02,
        SetAggregator = 0x04,
        SetTimeOrigin = 0x08
    }

    private readonly IList<ITimeSeriesComponent> _components;
    private IFt? _currentFn;
    private ITimeSeriesAggregator? _currentReducer;
    private AllowedTimeSeriesBuilderActions _currentAllowedActions;
    private TimeOrigin? _currentTimeOrigin;
    private bool _isInitialized;

    public static IValueGeneratorSelectionStage FromTimeSeries(TimeSeries timeSeries, 
        TimeOrigin? timeOrigin = null)
    {
        return new TimeSeriesBuilder(timeSeries, timeOrigin);
    }

    public static ITimeSeriesSelectionStage New()
    {
        return new TimeSeriesBuilder();
    }

    private TimeSeriesBuilder(TimeSeries timeSeries, TimeOrigin? timeOrigin)
    {
        _components = timeSeries.Components.ToList();
        _currentTimeOrigin = timeOrigin ?? timeSeries.TimeOrigin;
        _currentAllowedActions = AllowedTimeSeriesBuilderActions.SetAggregator 
                                 | AllowedTimeSeriesBuilderActions.SetFn;
    }

    private TimeSeriesBuilder()
    {
        _components = new List<ITimeSeriesComponent>();
        _currentAllowedActions = AllowedTimeSeriesBuilderActions.SetFn;
    }

    private void ResetCurrentSelection()
    {
        _currentFn = null;
        _currentReducer = null;
    }

    public IValueGeneratorSelectionStage SetFn(IFt fx)
    {
        if (!_currentAllowedActions.HasFlag(AllowedTimeSeriesBuilderActions.SetFn))
        {
            throw new Exception("Invalid!");
            //Invalid!
        }

        if (_isInitialized && _currentReducer == null)
        {
            //TODO meaningful exception!
            throw new Exception("Invalid as hell!");
        }

        _currentFn = fx;

        if (!_isInitialized)
        {
            _components.Add(new FtProxyComponent(_currentFn));
            _isInitialized = true;
        }
        else
        {
            var newElement = new TimeSeriesAggregatorComponent(_currentFn, _currentReducer!);
            _components.Add(newElement);
        }

        ResetCurrentSelection();

        //After setting a TimeSeries the user is only allowed to set either a transformer or a combiner.
        _currentAllowedActions =
            AllowedTimeSeriesBuilderActions.SetTransformer | AllowedTimeSeriesBuilderActions.SetAggregator;

        return this;
    }

    public IValueGeneratorSelectionStage SetTransformer(ITimeSeriesTransformer transformer)
    {
        if (!_currentAllowedActions.HasFlag(AllowedTimeSeriesBuilderActions.SetTransformer))
        {
            //Invalid!
            throw new Exception("LOL Fail");
        }

        var newElement = new TimeSeriesTransformerComponent(transformer);
        _components.Add(newElement);

        ResetCurrentSelection();

        _currentAllowedActions =
            AllowedTimeSeriesBuilderActions.SetTransformer |
            AllowedTimeSeriesBuilderActions.SetAggregator |
            AllowedTimeSeriesBuilderActions.SetTimeOrigin;

        return this;
    }

    public ITimeSeriesSelectionStage SetAggregator(ITimeSeriesAggregator aggregator)
    {
        if (!_currentAllowedActions.HasFlag(AllowedTimeSeriesBuilderActions.SetAggregator))
        {
            //Invalid!
        }

        _currentReducer = aggregator;

        //After setting a Combiner the user must set a TimeSeries.
        _currentAllowedActions = AllowedTimeSeriesBuilderActions.SetFn;

        return this;
    }

    public ICanBuildTimeSeries SetTimeOrigin(TimeOrigin timeOrigin)
    {
        if (_currentTimeOrigin != null)
        {
            //TODO this is invalid!
        }

        if (!_currentAllowedActions.HasFlag(AllowedTimeSeriesBuilderActions.SetTimeOrigin))
        {
            //TODO Invalid!
        }

        _currentTimeOrigin = timeOrigin;
        _currentAllowedActions = AllowedTimeSeriesBuilderActions.None;

        return this;
    }

    public TimeSeries Build()
    {
        if (!_components.Any())
        {
            //TODO real exception!
            throw new Exception("Invalid because no elements!");
        }

        return new TimeSeries(_components, _currentTimeOrigin);
    }
}