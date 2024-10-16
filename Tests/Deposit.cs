using Domain;
using InMemoryRepository;

namespace Tests;

public class Tests
{
    private TransactionService _transactionService;
    
    [SetUp]
    public void Setup()
    {
        ITransactionRepository transactionRepo = new InMemoryTransactionRepository();
        _transactionService = new TransactionService(transactionRepo);
    }

    [Test]
    public void CanNotDepositNegativeAmount()
    {
        var accountNumber = "123";
        var amount = -50;

        var depositResult = _transactionService.Deposit(accountNumber, amount);
        
        Assert.True(depositResult.Failed());
    }
    
    [Test]
    public void CanDepositIntoAccount()
    {
        var accountNumber = "123";
        var amount = 60.10m;

        _transactionService.Deposit(accountNumber, amount);

        var balanceResult = _transactionService.Balance(accountNumber);
        
        Assert.That(balanceResult, Is.EqualTo(amount));
    }
    
    [Test]
    public void CanHaveTwoAccountsDeposit()
    {
        var accountNumber1 = "123";
        var accountNumber2 = "456";
        var amount1 = 20.10m;
        var amount2 = 13.99m;

        _transactionService.Deposit(accountNumber1, amount1);
        _transactionService.Deposit(accountNumber2, amount2);

        var balanceResult1 = _transactionService.Balance(accountNumber1);
        var balanceResult2 = _transactionService.Balance(accountNumber2);
        
        Assert.That(balanceResult1, Is.EqualTo(amount1));
        Assert.That(balanceResult2, Is.EqualTo(amount2));
    }
}