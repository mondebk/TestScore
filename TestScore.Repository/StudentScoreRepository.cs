using TestScore.Domain.Interfaces;
using TestScore.Domain.Entities;

namespace TestScore.Repository;

public class StudentScoreRepository : IStudentScoreRepository
{
    public Task Add(IEnumerable<Domain.Entities.StudentScore> testScores)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Domain.Entities.StudentScore>> GetWhere(Func<Domain.Entities.StudentScore, bool> predicate)
    {
        throw new NotImplementedException();
    }
}