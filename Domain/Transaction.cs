namespace Domain;

public class Transaction(string accountId, TransactionType type, decimal amount)
{
    public string AccountId { get; set; } = accountId;

    public TransactionType Type { get; set; } = type;

    public decimal Amount { get; set; } = amount;

    public TransactionSource? Source { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public Transaction(string accountId, TransactionType type, decimal amount, TransactionSource source) 
        : this(accountId, type, amount)
    {
        Source = source;
    }
}