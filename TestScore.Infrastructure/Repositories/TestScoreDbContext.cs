using Microsoft.EntityFrameworkCore;
using TestScore.Infrastructure.StudentScore;
using TestScore.Infrastructure.TestScoreFile;

namespace TestScore.Infrastructure;

public class TestScoreDbContext : DbContext
{
    public DbSet<StudentScoreEntity> StudentScores => Set<StudentScoreEntity>();
    
    public DbSet<TestScoreFileEntity> TestScoreFiles => Set<TestScoreFileEntity>();

    public TestScoreDbContext(DbContextOptions<TestScoreDbContext> options) : base(options)
    {
        
    }
}