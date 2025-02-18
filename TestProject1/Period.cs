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
        if (End < budget.FirstDay() || Start > budget.LastDay())
        {
            return 0;
        }

        var effectiveStart = Start > budget.FirstDay() ? Start : budget.FirstDay();
        var effectiveEnd = End < budget.LastDay() ? End : budget.LastDay();

        return (effectiveEnd - effectiveStart).Days + 1;
    }
}