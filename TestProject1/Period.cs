namespace ConsoleApp1;

public class Period
{
    public Period(DateTime start, DateTime end)
    {
        Start = start;
        End = end;
    }

    public DateTime End { get; private set; }

    public DateTime Start { get; private set; }

    public int OverlappingDays(Period another)
    {
        if (End < another.Start || Start > another.End)
        {
            return 0;
        }

        var effectiveStart = Start > another.Start ? Start : another.Start;
        var effectiveEnd = End < another.End ? End : another.End;

        return (effectiveEnd - effectiveStart).Days + 1;
    }
}