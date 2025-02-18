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

    public int OverlappingDays(Budget budget)
    {
        var firstDay = budget.FirstDay();
        var lastDay = budget.LastDay();
        if (End < firstDay || Start > lastDay)
        {
            return 0;
        }

        var effectiveStart = Start > firstDay ? Start : firstDay;
        var effectiveEnd = End < lastDay ? End : lastDay;

        return (effectiveEnd - effectiveStart).Days + 1;
    }
}