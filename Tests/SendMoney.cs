using Domain;
using InMemoryRepository;

namespace Tests;

public class SendMoney
{
    [Test]
    public void CanSendMoneyToAccount()
    {
        var payerAccount = "123";
        var receiverAccount = "456";
        var amount = 45.99m;
        
        var transactionService = new TransactionService(new InMemoryTransactionRepository());

        transactionService.Deposit(payerAccount, amount);
        var sendResult = transactionService.Send(payerAccount, receiverAccount, amount);
        var balance = transactionService.Balance(receiverAccount);
        
        Assert.That(sendResult.Failed(), Is.False);
        Assert.That(balance, Is.EqualTo(amount));
    }

    [Test]
    public void CanNotSendMoreMoneyThanBalance()
    {
        var payerAccount = "123";
        var receiverAccount = "456";
        var depositAmount = 20.99m;
        var sendAmount = 45.00m;
        
        var transactionService = new TransactionService(new InMemoryTransactionRepository());

        transactionService.Deposit(payerAccount, depositAmount);
        var sendResult = transactionService.Send(payerAccount, receiverAccount, sendAmount);
        
        Assert.That(sendResult.Failed(), Is.True);
    }

    [Test]
    public void CanNotSendNegativeMoney()
    {
        var payerAccount = "123";
        var receiverAccount = "456";
        var amount = -20.99m;
        
        var transactionService = new TransactionService(new InMemoryTransactionRepository());

        transactionService.Deposit(payerAccount, amount);
        var sendResult = transactionService.Send(payerAccount, receiverAccount, amount);
        
        Assert.That(sendResult.Failed(), Is.True);
    }
}