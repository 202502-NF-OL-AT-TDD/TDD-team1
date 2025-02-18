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
            var monthEnd = LastDay(budget);

            if (end < budget.FirstDay() || start > monthEnd)
            {
                continue; // 該月不在查詢範圍內
            }

            var effectiveStart = start > budget.FirstDay() ? start : budget.FirstDay();
            var effectiveEnd = end < monthEnd ? end : monthEnd;

            int daysInMonth = DateTime.DaysInMonth(budget.FirstDay().Year, budget.FirstDay().Month);
            int effectiveDays = (effectiveEnd - effectiveStart).Days + 1;

            totalAmount += (budget.Amount / daysInMonth) * effectiveDays;
        }

        return totalAmount;
    }

    private static DateTime LastDay(Budget budget)
    {
        var monthEnd = budget.FirstDay().AddMonths(1).AddDays(-1);
        return monthEnd;
    }
}