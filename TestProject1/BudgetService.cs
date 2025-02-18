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
            if (end < budget.FirstDay() || start > budget.LastDay())
            {
                continue; // 該月不在查詢範圍內
            }

            var effectiveStart = start > budget.FirstDay() ? start : budget.FirstDay();
            var effectiveEnd = end < budget.LastDay() ? end : budget.LastDay();

            int effectiveDays;
            effectiveDays = (effectiveEnd - effectiveStart).Days + 1;
            var daysInMonth = DateTime.DaysInMonth(budget.FirstDay().Year, budget.FirstDay().Month);

            totalAmount += (budget.Amount / daysInMonth) * effectiveDays;
        }

        return totalAmount;
    }
}