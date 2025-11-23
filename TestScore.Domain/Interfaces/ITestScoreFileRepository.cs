using TestScore.Domain.Entities;

namespace TestScore.Domain.Interfaces;

public interface ITestScoreFileRepository
{
    public Task Add(TestScoreFile file, CancellationToken cancellationToken = default);
}