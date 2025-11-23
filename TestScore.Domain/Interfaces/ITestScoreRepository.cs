namespace TestScore.Domain.Interfaces;

public interface ITestScoreRepository
{
    public Task Add(IEnumerable<Entities.TestScore> testScores);
    
    public Task<IEnumerable<Entities.TestScore>> GetWhere(Func<Entities.TestScore, bool> predicate);
}