using TestScore.Domain.Interfaces;
using TestScore.Domain.Entities;

namespace TestScore.Repository;

public class TestScoreRepository : ITestScoreRepository
{
    public Task Add(IEnumerable<Domain.Entities.TestScore> testScores)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Domain.Entities.TestScore>> GetWhere(Func<Domain.Entities.TestScore, bool> predicate)
    {
        throw new NotImplementedException();
    }
}