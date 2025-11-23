using TestScore.Domain.Entities;

namespace TestScore.Domain.Interfaces;

public interface IStudentScoreRepository
{
    public Task Add(IEnumerable<StudentScore> studentScores);
    
    public Task<IEnumerable<StudentScore>> GetWhere(Func<StudentScore, bool> predicate);
}