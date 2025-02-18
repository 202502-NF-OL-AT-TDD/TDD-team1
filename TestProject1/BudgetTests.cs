using ConsoleApp1;
using NSubstitute;
using NUnit.Framework;

namespace TestProject1;

[TestFixture]
public class BudgetServiceTests
{
    private IBudgetRepo _budgetRepo;
    private BudgetService _budgetService;

    [SetUp]
    public void Setup()
    {
        _budgetRepo = Substitute.For<IBudgetRepo>();
        _budgetService = new BudgetService(_budgetRepo);
    }

    [Test]
    public void invalid_date_range()
    {
        var budgetReport = Substitute.For<IBudgetRepo>();

        budgetReport.GetAll().Returns(new List<Budget>
        {
            new Budget { YearMonth = "202505", Amount = 31000m }
        });
        
        _budgetService = new BudgetService(budgetReport);
        var actual = _budgetService.Query(new DateTime(2025, 05, 03), new DateTime(2025, 05, 01));

        Assert.That(actual, Is.EqualTo(0));
    }
    
    [Test]
    public void query_one_day()
    {
        var budgetReport = Substitute.For<IBudgetRepo>();

        budgetReport.GetAll().Returns(new List<Budget>
        {
            new Budget { YearMonth = "202505", Amount = 31000m }
        });
        
        _budgetService = new BudgetService(budgetReport);
        var actual = _budgetService.Query(new DateTime(2025, 05, 03), new DateTime(2025, 05, 03));

        Assert.That(actual, Is.EqualTo(1000));
    }
    
    [Test]
    public void query_partial_months()
    {
        var budgetReport = Substitute.For<IBudgetRepo>();

        budgetReport.GetAll().Returns(new List<Budget>
        {
            new Budget { YearMonth = "202505", Amount = 31000m }
        });
        
        _budgetService = new BudgetService(budgetReport);
        var actual = _budgetService.Query(new DateTime(2025, 05, 03), new DateTime(2025, 05, 04));

        Assert.That(actual, Is.EqualTo(2000));
    }
    
    [Test]
    public void query_full_months()
    {
        var budgetReport = Substitute.For<IBudgetRepo>();

        budgetReport.GetAll().Returns(new List<Budget>
        {
            new Budget { YearMonth = "202505", Amount = 31000m }
        });
        
        _budgetService = new BudgetService(budgetReport);
        var actual = _budgetService.Query(new DateTime(2025, 05, 01), new DateTime(2025, 05, 31));

        Assert.That(actual, Is.EqualTo(31000));
    }
    
    
    
    [Test]
    public void query_cross_months()
    {
        var budgetReport = Substitute.For<IBudgetRepo>();

        budgetReport.GetAll().Returns(new List<Budget>
        {
            new Budget { YearMonth = "202504", Amount = 60000m },
            new Budget { YearMonth = "202505", Amount = 31000m }
        });
        
        _budgetService = new BudgetService(budgetReport);
        var actual = _budgetService.Query(new DateTime(2025, 04, 30), new DateTime(2025, 05, 01));

        Assert.That(actual, Is.EqualTo(3000));
    }
    
}