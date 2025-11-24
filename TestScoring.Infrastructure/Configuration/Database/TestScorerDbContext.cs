using Microsoft.EntityFrameworkCore;
using TestScoring.Infrastructure.Models;

namespace TestScoring.Infrastructure.Configuration.Database;

public class TestScorerDbContext : DbContext
{
    public DbSet<TestScoreEntity> StudentScores => Set<TestScoreEntity>();
    
    public DbSet<TestScoreFileEntity> TestScoreFiles => Set<TestScoreFileEntity>();

    public TestScorerDbContext(DbContextOptions<TestScorerDbContext> options) : base(options)
    {
        
    }
}