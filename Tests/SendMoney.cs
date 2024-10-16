using Domain;
using Microsoft.EntityFrameworkCore;
using SqlRepository;
using AppContext = SqlRepository.AppContext;

namespace Tests;

public class SendMoney
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