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
            var effectiveDays = OverlappingDays(new Period(start, end), budget);

            var daysInMonth = DateTime.DaysInMonth(budget.FirstDay().Year, budget.FirstDay().Month);

            totalAmount += (budget.Amount / daysInMonth) * effectiveDays;
        }

        return totalAmount;
    }

    private static int OverlappingDays(Period period, Budget budget)
    {
        if (period.End < budget.FirstDay() || period.Start > budget.LastDay())
        {
            return 0;
        }

        var effectiveStart = period.Start > budget.FirstDay() ? period.Start : budget.FirstDay();
        var effectiveEnd = period.End < budget.LastDay() ? period.End : budget.LastDay();

        return (effectiveEnd - effectiveStart).Days + 1;
    }
}