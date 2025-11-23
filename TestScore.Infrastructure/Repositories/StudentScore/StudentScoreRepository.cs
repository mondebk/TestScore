using Microsoft.EntityFrameworkCore;
using TestScore.Domain.Interfaces;

namespace TestScore.Infrastructure.StudentScore;

public class StudentScoreRepository : IStudentScoreRepository
{
    private readonly TestScoreDbContext _dbContext;

    public StudentScoreRepository(TestScoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(IEnumerable<Domain.Entities.StudentScore> studentScores,
        CancellationToken cancellationToken = default)
    {
        var studentScoreEntities = studentScores
            .Select(studentScore => StudentScoreMapper.ToEntity(studentScore));

        await _dbContext.StudentScores.AddRangeAsync(studentScoreEntities, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<Domain.Entities.StudentScore>> GetTopScore(
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

    public async Task<IEnumerable<Domain.Entities.StudentScore>> SearchScore(string searchTerm,
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