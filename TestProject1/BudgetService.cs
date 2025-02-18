namespace ConsoleApp1;

public class BudgetService
{
    private readonly IBudgetRepo _budgetRepo;

    public BudgetService(IBudgetRepo budgetRepo)
    {
        _budgetRepo = budgetRepo;
    }

    public decimal Query(DateTime start, DateTime end)
    {
        if (start > end)
        {
            return 0;
        }

        var budgets = _budgetRepo.GetAll();
        decimal totalAmount = 0;

        foreach (var budget in budgets)
        {
            var isYearMonthValid = DateTime.TryParseExact(budget.YearMonth + "01", "yyyyMMdd", null,
                System.Globalization.DateTimeStyles.None, out DateTime budgetMonth);
            if (!isYearMonthValid)
            {
                continue; // 無效的 YearMonth 格式，跳過
            }

            var monthStart = new DateTime(budgetMonth.Year, budgetMonth.Month, 1);
            var monthEnd = monthStart.AddMonths(1).AddDays(-1);

            if (end < monthStart || start > monthEnd)
            {
                continue; // 該月不在查詢範圍內
            }

            var effectiveStart = start > monthStart ? start : monthStart;
            var effectiveEnd = end < monthEnd ? end : monthEnd;

            int daysInMonth = DateTime.DaysInMonth(budgetMonth.Year, budgetMonth.Month);
            int effectiveDays = (effectiveEnd - effectiveStart).Days + 1;

            totalAmount += (budget.Amount / daysInMonth) * effectiveDays;
        }

        return totalAmount;
    }
}