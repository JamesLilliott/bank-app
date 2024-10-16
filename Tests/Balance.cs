using Domain;
using InMemoryRepository;

namespace Tests;

public class Balance
{
    private TransactionService _transactionService;
    
    [SetUp]
    public void Setup()
    {
        ITransactionRepository transactionRepo = new InMemoryTransactionRepository();
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