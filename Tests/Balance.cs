using Domain;

namespace Tests;

public class Balance
{
    [Test]
    public void CanGetBalance()
    {
        var account1 = "123";
        
        var transactionService = new TransactionService();

        var balance = transactionService.Balance(account1);
        
        Assert.That(balance, Is.EqualTo(0m));
    }
    
    [Test]
    public void CanGetBalanceAfterDeposit()
    {
        var account1 = "123";
        var amount1 = 10.99m;
        
        var transactionService = new TransactionService();

        transactionService.Deposit(account1, amount1);
        var balance = transactionService.Balance(account1);
        
        Assert.That(balance, Is.EqualTo(amount1));
    }
}