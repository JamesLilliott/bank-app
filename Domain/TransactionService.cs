namespace Domain;

public class TransactionService
{
    //private Dictionary<string, decimal> _bank = new Dictionary<string, decimal>();
    private List<Transaction> _transactionLog = new List<Transaction>();
    
    public DataResult<bool> Deposit(string accountNumber, decimal amount)
    {
        if (amount < 0.00m)
        {
            return Result.Failure<bool>("Amount can not be negative");
        }
        
        _transactionLog.Add(new Transaction(accountNumber, TransactionType.Credit, amount, TransactionSource.Deposit));

        return Result.Success(true);
    }

    public decimal Balance(string accountNumber)
    {
        var creditAmount = _transactionLog
            .Where(x => x.AccountId == accountNumber)
            .Where(x => x.Type == TransactionType.Credit)
            .Sum(x => x.Amount);
        
        var debitAmount = _transactionLog
            .Where(x => x.AccountId == accountNumber)
            .Where(x => x.Type == TransactionType.Debit)
            .Sum(x => x.Amount);

        return creditAmount - debitAmount;
    }

    public DataResult<decimal> Withdraw(string accountNumber, decimal amount)
    {
        if (amount < 0.00m)
        {
            return Result.Failure<decimal>("Amount can not be negative");
        }
        
        var balance = Balance(accountNumber);
        if (balance < amount)
        {
            return Result.Failure<decimal>("Not enough money in account");
        }
        
        _transactionLog.Add(new Transaction(accountNumber, TransactionType.Debit, amount));
        
        return Result.Success(amount);
    }

    public DataResult<bool> Send(string from, string to, decimal amount)
    {
        if (amount < 0.00m)
        {
            return Result.Failure<bool>("Amount can not be negative");
        }
        
        var withdrawalResult = Withdraw(from, amount);
        if (withdrawalResult.Failed())
        {
            return Result.Failure<bool>(withdrawalResult.ErrorMessage ?? "Can not send more money than balance");
        }
        
        _transactionLog.Add(new Transaction(from, TransactionType.Debit, amount, TransactionSource.ReceiveMoney));
        
        return Deposit(to, withdrawalResult.Value);
    }
}