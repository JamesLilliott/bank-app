using Domain;

namespace InMemoryRepository;

public class InMemoryTransactionRepository : ITransactionRepository
{
    private List<Transaction> _transactionLog = new List<Transaction>();

    public void Add(Transaction transaction)
    {
        _transactionLog.Add(transaction);
    }

    public IEnumerable<Transaction> Get(string accountId)
    {
        return _transactionLog.Where(x => x.AccountId == accountId);
    }
}