using System;

namespace FluentTimeSeries.Demo;

internal class Example
{
    public int Id { get; }

    public string Name { get; }

    public Action Run { get; }

    public Example(int id, string name, Action run)
    {
        Id = id;
        Name = name;
        Run = run;
    }
}