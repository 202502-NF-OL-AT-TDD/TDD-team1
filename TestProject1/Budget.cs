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
}