using Domain;
using Microsoft.EntityFrameworkCore;

namespace SqlRepository;

public class AppContext : DbContext
{
    public DbSet<Transaction> Transactions { get; set; }
    
    public AppContext(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Transaction>()
            .Property(x => x.TransactionId)
            .ValueGeneratedOnAdd();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=/Users/jameslilliott/Projects/c#/TransactionApp/TransactionApp/app.db");
        }
    }
}