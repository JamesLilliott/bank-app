using Domain;
using InMemoryRepository;

namespace Tests;

public class Withdraw
{
    private TransactionService _transactionService;

    [SetUp]
    public void Setup()
    {
        ITransactionRepository transactionRepo = new InMemoryTransactionRepository();
        _transactionService = new TransactionService(transactionRepo);
    }
    
    [Test]
    public void CanWithdrawMoney()
    {
        var accountNumber1 = "123";
        var amount1 = 20.10m;

        _transactionService.Deposit(accountNumber1, amount1);
        var withdrawResult = _transactionService.Withdraw(accountNumber1, amount1);
        
        Assert.That(withdrawResult.Value, Is.EqualTo(amount1));
    }
    
    [Test]
    public void CanTwoAccountsWithdrawMoney()
    {
        var accountNumber1 = "123";
        var accountNumber2 = "456";
        var amount1 = 20.10m;
        var amount2 = 14.88m;

        _transactionService.Deposit(accountNumber1, amount1);
        var withdrawResult1 = _transactionService.Withdraw(accountNumber1, amount1);
        
        _transactionService.Deposit(accountNumber2, amount2);
        var withdrawResult2 = _transactionService.Withdraw(accountNumber2, amount2);
        
        Assert.That(withdrawResult1.Value, Is.EqualTo(amount1));
        Assert.That(withdrawResult2.Value, Is.EqualTo(amount2));
    }
    
    [Test]
    public void CanNotWithdrawBelowBalance()
    {
        var accountNumber1 = "123";
        var amount1 = 20.10m;
        
        var withdrawResult1 = _transactionService.Withdraw(accountNumber1, amount1);
        
        Assert.That(withdrawResult1.Failed(), Is.EqualTo(true));
    }
    
    [Test]
    public void CanNotWithdrawBelowBalanceWithTwoWithdrawals()
    {
        var accountNumber1 = "123";
        var amount1 = 20.10m;
        
        _transactionService.Deposit(accountNumber1, amount1);
        
        var withdrawResult1 = _transactionService.Withdraw(accountNumber1, amount1);
        var withdrawResult2 = _transactionService.Withdraw(accountNumber1, amount1);
        
        Assert.That(withdrawResult1.Value, Is.EqualTo(amount1));
        Assert.That(withdrawResult2.Failed(), Is.EqualTo(true));
    }
    
    [Test]
    public void CanNotWithdrawNegativeAmount()
    {
        var accountNumber1 = "123";
        var amount1 = -20.10m;
        
        var withdrawResult1 = _transactionService.Withdraw(accountNumber1, amount1);
        
        Assert.That(withdrawResult1.Failed(), Is.EqualTo(true));
    }
}