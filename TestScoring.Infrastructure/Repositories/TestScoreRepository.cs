using Microsoft.EntityFrameworkCore;
using TestScoring.Domain.Entities;
using TestScoring.Domain.Interfaces.Repositories;
using TestScoring.Infrastructure.Configuration.Database;
using TestScoring.Infrastructure.Mappers;

namespace TestScoring.Infrastructure.Repositories;

public class TestScoreRepository : ITestScoreRepository
{
    private readonly TestScorerDbContext _dbContext;

    public TestScoreRepository(TestScorerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(IEnumerable<TestScore> studentScores,
        CancellationToken cancellationToken = default)
    {
        var studentScoreEntities = studentScores
            .Select(studentScore => StudentScoreMapper.ToEntity(studentScore));

        await _dbContext.StudentScores.AddRangeAsync(studentScoreEntities, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<TestScore>> GetTopScore(
        CancellationToken cancellationToken = default)
    {
        var topScore = await _dbContext.StudentScores
            .MaxAsync(studentScore => studentScore.Score, cancellationToken);

        var topScoreEntities = await _dbContext.StudentScores
            .Where(studentScore => studentScore.Score == topScore)
            .OrderBy(studentScore => studentScore.FirstName)
            .ThenBy(studentScore => studentScore.LastName)
            .ToListAsync(cancellationToken);

        return topScoreEntities.Select(topScore => StudentScoreMapper.ToDomain(topScore));
    }

    public async Task<IEnumerable<TestScore>> SearchScore(string searchTerm,
        CancellationToken cancellationToken = default)
    {
        var searchResults = await _dbContext.StudentScores
            .Where(studentScore => studentScore.FirstName.Contains(searchTerm) || studentScore.LastName.Contains(searchTerm))
            .OrderBy(studentScore => studentScore.FirstName)
            .ThenBy(studentScore => studentScore.LastName)
            .ToListAsync(cancellationToken);

        return searchResults.Select(searchResult => StudentScoreMapper.ToDomain(searchResult));
    }
}