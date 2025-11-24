using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using TestScoring.Infrastructure.Configuration.Database;

namespace TestScoring.Infrastructure;

public class DesignTimeFactory : IDesignTimeDbContextFactory<TestScorerDbContext>
{
    public TestScorerDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<TestScorerDbContext>()
            .UseSqlite("Data Source=testscoreapp.db")
            .Options;

        return new TestScorerDbContext(options);
    }
}