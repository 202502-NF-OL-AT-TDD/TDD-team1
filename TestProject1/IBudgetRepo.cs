namespace ConsoleApp1;

public interface IBudgetRepo
{
    public IEnumerable<Budget> GetAll();
}