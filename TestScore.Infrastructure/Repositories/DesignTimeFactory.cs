using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TestScore.Infrastructure;

public class DesignTimeFactory : IDesignTimeDbContextFactory<TestScoreDbContext>
{
    public TestScoreDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<TestScoreDbContext>()
            .UseSqlite("Data Source=testscoreapp.db")
            .Options;

        return new TestScoreDbContext(options);
    }
}