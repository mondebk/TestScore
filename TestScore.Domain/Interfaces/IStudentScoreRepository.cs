using TestScore.Domain.Entities;

namespace TestScore.Domain.Interfaces;

public interface IStudentScoreRepository
{
    public Task Add(IEnumerable<StudentScore> studentScores, CancellationToken cancellationToken = default);

    public Task<IEnumerable<StudentScore>> GetTopScore(CancellationToken cancellationToken = default);
    
    public Task<IEnumerable<StudentScore>> SearchScore(string searchTerm, CancellationToken cancellationToken = default);
}