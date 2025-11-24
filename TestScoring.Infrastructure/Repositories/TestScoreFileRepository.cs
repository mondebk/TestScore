using TestScoring.Domain.Entities;
using TestScoring.Domain.Interfaces.Repositories;
using TestScoring.Infrastructure.Configuration.Database;
using TestScoring.Infrastructure.Mappers;

namespace TestScoring.Infrastructure.Repositories;

public class TestScoreFileRepository : ITestScoreFileRepository
{
    private readonly TestScoringDbContext _dbContext;
    
    public TestScoreFileRepository(TestScoringDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task Add(TestScoreFile testScoreFile, CancellationToken cancellationToken)
    {
        var testScoreEntity = TestScoreFileMapper.ToEntity(testScoreFile);
        
        await _dbContext.AddAsync(testScoreEntity, cancellationToken);
    }
}