using Microsoft.EntityFrameworkCore;
using TestScoring.Domain.Entities;
using TestScoring.Domain.Interfaces.Repositories;
using TestScoring.Infrastructure.Configuration.Database;
using TestScoring.Infrastructure.Mappers;

namespace TestScoring.Infrastructure.Repositories;

public class TestScoreRepository : ITestScoreRepository
{
    private readonly TestScoringDbContext _dbContext;

    public TestScoreRepository(TestScoringDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(
        IEnumerable<TestScore> testScores,
        CancellationToken cancellationToken = default)
    {
        var testScoreEntities = testScores
            .Select(testScore => TestScoreMapper.ToEntity(testScore));

        await _dbContext.TestScores.AddRangeAsync(testScoreEntities, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<TestScore>> GetTopScore(CancellationToken cancellationToken = default)
    {
        var topScore = await _dbContext.TestScores
            .MaxAsync(testScore => testScore.Score, cancellationToken);

        var topScoreEntities = await _dbContext.TestScores
            .Where(testScore => testScore.Score == topScore)
            .OrderBy(testScore => testScore.FirstName)
            .ThenBy(testScore => testScore.LastName)
            .ToListAsync(cancellationToken);

        return topScoreEntities.Select(testScoreEntity => TestScoreMapper.ToDomain(testScoreEntity));
    }

    public async Task<IEnumerable<TestScore>> SearchScore(
        string searchTerm,
        CancellationToken cancellationToken = default)
    {
        var searchResults = await _dbContext.TestScores
            .Where(testScore =>
                testScore.FirstName.Contains(searchTerm) || testScore.LastName.Contains(searchTerm))
            .GroupBy(testScore => new { testScore.FirstName, testScore.LastName })
            .Select(group => group.OrderByDescending(testScore => testScore.Score).First())
            .OrderBy(testScore => testScore.FirstName)
            .ThenBy(testScore => testScore.LastName)
            .ToListAsync(cancellationToken);

        return searchResults.Select(searchResult => TestScoreMapper.ToDomain(searchResult));
    }
}