namespace TestProject1;

public class Budget
{
    public decimal Amount { get; set; } // 該月的總預算金額
    public string YearMonth { get; set; } // 格式為 "yyyyMM"

    public decimal OverlappingAmount(Period period)
    {
        return DailyAmount() * period.OverlappingDays(CreatePeriod());
    }

    private Period CreatePeriod()
    {
        return new Period(FirstDay(), LastDay());
    }

    private decimal DailyAmount()
    {
        return Amount / Days();
    }

    private int Days()
    {
        return DateTime.DaysInMonth(FirstDay().Year, FirstDay().Month);
    }

    private DateTime FirstDay()
    {
        return DateTime.ParseExact(YearMonth + "01", "yyyyMMdd", null,
            System.Globalization.DateTimeStyles.None);
    }

    private DateTime LastDay()
    {
        return new DateTime(FirstDay().Year, FirstDay().Month, Days());
    }
}