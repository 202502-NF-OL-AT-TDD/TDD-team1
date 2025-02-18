namespace ConsoleApp1;

public class Budget
{
    public decimal Amount { get; set; } // 該月的總預算金額
    public string YearMonth { get; set; } // 格式為 "yyyyMM"

    public DateTime FirstDay()
    {
        return DateTime.ParseExact(YearMonth + "01", "yyyyMMdd", null,
            System.Globalization.DateTimeStyles.None);
    }

    public DateTime LastDay()
    {
        return FirstDay().AddMonths(1).AddDays(-1);
    }

    public Period CreatePeriod()
    {
        return new Period(FirstDay(), LastDay());
    }

    public int Days()
    {
        return DateTime.DaysInMonth(FirstDay().Year, FirstDay().Month);
    }

    public decimal DailyAmount()
    {
        return Amount / Days();
    }

    public decimal OverlappingAmount(Period period)
    {
        return DailyAmount() * period.OverlappingDays(CreatePeriod());
    }
}