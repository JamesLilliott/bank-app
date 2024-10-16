using Domain;
using Microsoft.EntityFrameworkCore;

namespace SqlRepository;

public class SqlTransactionRepository : ITransactionRepository
{
    private AppContext _context;

    public SqlTransactionRepository(string connectionString)
    {
        var builder = new DbContextOptionsBuilder();
        builder.UseSqlite(connectionString);
        
        _context = new AppContext(builder.Options);
    }
    
    public SqlTransactionRepository(AppContext context)
    {
        _context = context;
    }

    public void Add(Transaction transaction)
    {
        _context.Transactions.Add(transaction);
        _context.SaveChanges();
    }

    public IEnumerable<Transaction> Get(string accountId)
    {
        return _context.Transactions.Where(x => x.AccountId == accountId);
    }
}