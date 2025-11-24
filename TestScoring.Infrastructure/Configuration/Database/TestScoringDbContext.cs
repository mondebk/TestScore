using Microsoft.EntityFrameworkCore;
using TestScoring.Infrastructure.Models;

namespace TestScoring.Infrastructure.Configuration.Database;

public class TestScoringDbContext : DbContext
{
    public DbSet<TestScoreEntity> TestScores => Set<TestScoreEntity>();
    
    public DbSet<TestScoreFileEntity> TestScoreFiles => Set<TestScoreFileEntity>();

    public TestScoringDbContext(DbContextOptions<TestScoringDbContext> options) : base(options)
    {
        
    }
}