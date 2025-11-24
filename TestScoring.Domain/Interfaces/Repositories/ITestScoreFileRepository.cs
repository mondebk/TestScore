using TestScoring.Domain.Entities;

namespace TestScoring.Domain.Interfaces.Repositories;

public interface ITestScoreFileRepository
{
    public Task Add(TestScoreFile testScoreFile, CancellationToken cancellationToken);
}