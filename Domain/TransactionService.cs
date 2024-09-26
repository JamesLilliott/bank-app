namespace Domain;

public class TransactionService
{
    private Dictionary<string, decimal> _bank = new Dictionary<string, decimal>();
    
    public DataResult<bool> Deposit(string accountNumber, decimal amount)
    {
        if (amount < 0.00m)
        {
            return Result.Failure<bool>("Amount can not be negative");
        }
        
        var balance = Balance(accountNumber);
        
        _bank[accountNumber] = balance + amount;

        return Result.Success(true);
    }

    public decimal Balance(string accountNumber)
    {
        var balance = _bank.GetValueOrDefault(accountNumber);
        
        return balance;
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
        
        _bank[accountNumber] = balance - amount;
        
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
        
        return Deposit(to, withdrawalResult.Value);
    }
}