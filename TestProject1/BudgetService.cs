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

        var period = new Period(start, end);
        foreach (var budget in budgets)
        {
            var overlappingDays = period.OverlappingDays(budget.CreatePeriod());

            var daysInMonth = budget.Days();

            totalAmount += (budget.Amount / daysInMonth) * overlappingDays;
        }

        return totalAmount;
    }
}