using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using TestScoring.Domain.Entities;
using TestScoring.Domain.Interfaces.Repositories;
using TestScoring.Infrastructure.Configuration.Database;

namespace TestScoring.Infrastructure.Repositories;

public class TestScoreFileRepository : ITestScoreFileRepository
{
    private readonly TestScorerDbContext dbContext;
    
    public TestScoreFileRepository(TestScorerDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    
    public Task Add(TestScoreFile testScoreFile, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}