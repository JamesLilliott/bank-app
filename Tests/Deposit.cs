using Domain;

namespace Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void CanNotDepositNegativeAmount()
    {
        var accountNumber = "123";
        var amount = -50;
        var transactionService = new TransactionService();

        var depositResult = transactionService.Deposit(accountNumber, amount);
        
        Assert.True(depositResult.Failed());
    }
    
    [Test]
    public void CanDepositIntoAccount()
    {
        var accountNumber = "123";
        var amount = 60.10m;
        var transactionService = new TransactionService();

        transactionService.Deposit(accountNumber, amount);

        var balanceResult = transactionService.Balance(accountNumber);
        
        Assert.That(balanceResult, Is.EqualTo(amount));
    }
    
    [Test]
    public void CanHaveTwoAccountsDeposit()
    {
        var accountNumber1 = "123";
        var accountNumber2 = "456";
        var amount1 = 20.10m;
        var amount2 = 13.99m;
        var transactionService = new TransactionService();

        transactionService.Deposit(accountNumber1, amount1);
        transactionService.Deposit(accountNumber2, amount2);

        var balanceResult1 = transactionService.Balance(accountNumber1);
        var balanceResult2 = transactionService.Balance(accountNumber2);
        
        Assert.That(balanceResult1, Is.EqualTo(amount1));
        Assert.That(balanceResult2, Is.EqualTo(amount2));
    }
}