using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SqlRepository;

public class AppContextFactory : IDesignTimeDbContextFactory<AppContext>
{
    public AppContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppContext>();
        optionsBuilder.UseSqlite("Data Source=/Users/jameslilliott/Projects/c#/TransactionApp/TransactionApp/app.db");

        return new AppContext(optionsBuilder.Options);
    }
}