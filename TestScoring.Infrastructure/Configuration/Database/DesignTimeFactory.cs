using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using TestScoring.Infrastructure.Configuration.Database;

namespace TestScoring.Infrastructure;

public class DesignTimeFactory : IDesignTimeDbContextFactory<TestScoringDbContext>
{
    public TestScoringDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<TestScoringDbContext>()
            .UseSqlite("Data Source=testscoreapp.db")
            .Options;

        return new TestScoringDbContext(options);
    }
}