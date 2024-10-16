using Domain;
using InMemoryRepository;

namespace Tests;

public class SendMoney
{
    private TransactionService _transactionService;

    [SetUp]
    public void Setup()
    {
        ITransactionRepository transactionRepo = new InMemoryTransactionRepository();
        _transactionService = new TransactionService(transactionRepo);
    }
    
    [Test]
    public void CanSendMoneyToAccount()
    {
        var payerAccount = "123";
        var receiverAccount = "456";
        var amount = 45.99m;
        
        _transactionService.Deposit(payerAccount, amount);
        var sendResult = _transactionService.Send(payerAccount, receiverAccount, amount);
        var balance = _transactionService.Balance(receiverAccount);
        
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

        _transactionService.Deposit(payerAccount, depositAmount);
        var sendResult = _transactionService.Send(payerAccount, receiverAccount, sendAmount);
        
        Assert.That(sendResult.Failed(), Is.True);
    }

    [Test]
    public void CanNotSendNegativeMoney()
    {
        var payerAccount = "123";
        var receiverAccount = "456";
        var amount = -20.99m;

        _transactionService.Deposit(payerAccount, amount);
        var sendResult = _transactionService.Send(payerAccount, receiverAccount, amount);
        
        Assert.That(sendResult.Failed(), Is.True);
    }
}