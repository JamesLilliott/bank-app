using Domain;
using Microsoft.EntityFrameworkCore;
using SqlRepository;
using AppContext = SqlRepository.AppContext;

namespace Tests;

public class Balance
{
    private TransactionService _transactionService;
    
    [SetUp]
    public void Setup()
    {
        var testName = $"{TestContext.CurrentContext.Test.ClassName}-{TestContext.CurrentContext.Test.Name}";
        var context = new AppContext(new DbContextOptionsBuilder<AppContext>()
            .UseInMemoryDatabase($"{testName}")
            .Options);
        
        ITransactionRepository transactionRepo = new SqlTransactionRepository(context);
        _transactionService = new TransactionService(transactionRepo);
    }
    
    [Test]
    public void CanGetBalance()
    {
        var account1 = "123";

        var balance = _transactionService.Balance(account1);
        
        Assert.That(balance, Is.EqualTo(0m));
    }
    
    [Test]
    public void CanGetBalanceAfterDeposit()
    {
        var account1 = "123";
        var amount1 = 10.99m;

        _transactionService.Deposit(account1, amount1);
        var balance = _transactionService.Balance(account1);
        
        Assert.That(balance, Is.EqualTo(amount1));
    }
}