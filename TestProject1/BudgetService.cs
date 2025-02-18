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
            var firstDay = DateTime.ParseExact(budget.YearMonth + "01", "yyyyMMdd", null,
                System.Globalization.DateTimeStyles.None);

            var monthStart = new DateTime(firstDay.Year, firstDay.Month, 1);
            var monthEnd = monthStart.AddMonths(1).AddDays(-1);

            if (end < monthStart || start > monthEnd)
            {
                continue; // 該月不在查詢範圍內
            }

            var effectiveStart = start > monthStart ? start : monthStart;
            var effectiveEnd = end < monthEnd ? end : monthEnd;

            int daysInMonth = DateTime.DaysInMonth(firstDay.Year, firstDay.Month);
            int effectiveDays = (effectiveEnd - effectiveStart).Days + 1;

            totalAmount += (budget.Amount / daysInMonth) * effectiveDays;
        }

        return totalAmount;
    }
}