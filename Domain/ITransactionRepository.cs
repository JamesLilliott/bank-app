namespace Domain;

public interface ITransactionRepository
{
    public void Add(Transaction transaction);
    
    public IEnumerable<Transaction> Get(string accountId);
}