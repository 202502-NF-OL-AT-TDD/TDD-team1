using TestProject1;

namespace ConsoleApp1;

public interface IBudgetRepo
{
    public IEnumerable<Budget> GetAll();
}