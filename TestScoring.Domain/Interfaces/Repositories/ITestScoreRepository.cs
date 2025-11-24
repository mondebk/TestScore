using TestScoring.Domain.Entities;

namespace TestScoring.Domain.Interfaces.Repositories;

public interface ITestScoreRepository
{
    Task Add(IEnumerable<TestScore> studentScores, CancellationToken cancellationToken);

    Task<IEnumerable<TestScore>> GetTopScore(CancellationToken cancellationToken);
    
    Task<IEnumerable<TestScore>> SearchScore(string searchTerm, CancellationToken cancellationToken);
}