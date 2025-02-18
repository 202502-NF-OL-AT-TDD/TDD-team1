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
            var effectiveDays = OverlappingDays(start, end, budget);

            var daysInMonth = DateTime.DaysInMonth(budget.FirstDay().Year, budget.FirstDay().Month);

            totalAmount += (budget.Amount / daysInMonth) * effectiveDays;
        }

        return totalAmount;
    }

    private static int OverlappingDays(DateTime start, DateTime end, Budget budget)
    {
        if (end < budget.FirstDay() || start > budget.LastDay())
        {
            return 0;
        }

        var effectiveStart = start > budget.FirstDay() ? start : budget.FirstDay();
        var effectiveEnd = end < budget.LastDay() ? end : budget.LastDay();

        return (effectiveEnd - effectiveStart).Days + 1;
    }
}